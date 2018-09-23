using Microsoft.CSharp;
using System;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection;


namespace CompileInMemory
{
    public class AppDomainProxy : MarshalByRefObject
    {
        // Borrowed a lot of code from https://www.codeproject.com/Tips/715891/Compiling-Csharp-Code-at-Runtime
        public static Assembly CompileCSharpCode(string code)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.Append(string.Format("Error [{0}]: {1}", error.ErrorNumber, error.ErrorText));
                }
                throw new InvalidOperationException(sb.ToString());
            }

            Assembly assembly = results.CompiledAssembly;
            Console.WriteLine("[+] Successfully compiled code");
            return assembly;
        }

        // Execute the assembly
        public static object Execute(Assembly assembly)
        {
            Type program = assembly.GetType("Program.Execute");
            MethodInfo main = program.GetMethod("Main");
            Console.WriteLine("[+] Loaded {0}", program);
            return main.Invoke(null, parameters: new object[] {"string"});
        }

        public static void Main(string[] args)
        {
            string cSharpCode = System.IO.File.ReadAllText(args[0]); 
            Assembly assembly = CompileCSharpCode(cSharpCode);
            string output = Execute(assembly).ToString();
            Console.Write(output);
        }
    }
}