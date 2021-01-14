param(
[Parameter(ValueFromPipeline=$true)]
[ValidateNotNullOrEmpty()]$csharpCode,
$OutFile
)
#Ref: https://blog.csdn.net/eva001206130/article/details/79679883
# $providerDict
$providerDict = New-Object 'System.Collections.Generic.Dictionary[[string],[string]]'
$providerDict.Add('CompilerVersion','v4.0')
$codeCompiler = [Microsoft.CSharp.CSharpCodeProvider]$providerDict
Add-Type -AssemblyName System.IO.Compression 
# Create the optional compiler parameters
$compilerParameters = New-Object 'System.CodeDom.Compiler.CompilerParameters'
$compilerParameters.GenerateExecutable = $true
$compilerParameters.GenerateInMemory = $true
$compilerParameters.WarningLevel = 3
$compilerParameters.TreatWarningsAsErrors = $false
$compilerParameters.CompilerOptions = '/optimize'
$outputExe = Join-Path $pwd $OutFile
$compilerParameters.OutputAssembly =  $outputExe
$compilerParameters.ReferencedAssemblies.Add( [System.Diagnostics.Process].Assembly.Location ) > $null
$compilerParameters.ReferencedAssemblies.Add("System.IO.compression.dll") > $null
$compilerParameters.ReferencedAssemblies.Add("System.Linq.dll") > $null
$compilerParameters.ReferencedAssemblies.Add("System.Xml.dll") > $null
$compilerParameters.ReferencedAssemblies.Add("System.Xml.Linq.dll") > $null
$compilerParameters.ReferencedAssemblies.Add("System.Collections.dll") > $null
$compilerParameters.ReferencedAssemblies.Add("System.IO.Compression.FileSystem.dll") > $null

# Compile Assembly
$compilerResult = $codeCompiler.CompileAssemblyFromSource($compilerParameters,$csharpCode)
Write-Output $compilerResult.Errors