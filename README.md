# CSharpTools

Random tools for used for some of the various projects I have worked on. Projects built with .Net 3.5 and VS 2017.

## CompileInMemory

Compiles C# code in memory. Built as a console app, takes a text file containing C# code as an argument. The C# code in the text file must have the "Main" function in Namespace "Program" and Class "Execute".

```shell
.\CompileInMemory.exe .\example.cs
```

Example.cs is a simply returns hello world as a string.

## NotPowerShell

Executes PowerShell without calling powershell.exe. Built as a console app, takes a text file containing PowerShell code as the first argument and executes it. Currently errors out if Write-Host is in the script.

```shell
.\NotPowerShell.exe .\example.ps1
```

Example.ps1 current calls the Get-Host cmdlet.