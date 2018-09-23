using System;
using System.Text;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace NotPowerShell
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string output = PowerShell.Execute(args[0]);
            Console.WriteLine(output);
        }
    }

    public class PowerShell
    {
        public static string Execute(string file)
        {
            string command = System.IO.File.ReadAllText(file);

            RunspaceConfiguration rspacecfg = RunspaceConfiguration.Create();
            Runspace rspace = RunspaceFactory.CreateRunspace(rspacecfg);
            rspace.Open();

            Pipeline pipeline = rspace.CreatePipeline();
            pipeline.Commands.AddScript(command);
            pipeline.Commands.Add("Out-String");
            Collection<PSObject> result = pipeline.Invoke();

            StringBuilder resultString = new StringBuilder();
            foreach (PSObject line in result)
            {
                resultString.Append(line);
            }
            return resultString.ToString();
        }
    }
}