cls

# '[p]sake' is the same as 'psake' but $Error is not polluted
remove-module [p]sake

# find psake's path
$psakeModule = (Get-ChildItem (".\WpfApplication1\Packages\psake*\tools\psake.psm1")).FullName | Sort-Object $_ | select -last 1

Import-Module $psakeModule

Invoke-psake -buildFile .\WpfApplication1\Build\default.ps1 -taskList Test -framework 4.0 -parameters @{"solutionFile" = "..\WpfApplication1.sln"}
#Invoke-psake -buildFile .\default.ps1 -taskList Test -parameters @{"solutionFile" = "D:\Projects\STR\GIT\STR-Refresh\src\SelecT.DMS\SelecT.DMS.sln"}

Write-Host "Build exit code:" $LastExitCode

# Propagating the exit code so that builds actually fail when there is a problem
exit $LastExitCode