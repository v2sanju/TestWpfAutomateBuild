cls

remove-module [p]sake

Import-Module ..\packages\psake.*\tools\psake.psm1

Invoke-psake -buildFile .\default.ps1 -taskList Test -framework 4.0 -parameters @{"solutionFile" = "..\WpfApplication1.sln"}
#Invoke-psake -buildFile .\default.ps1 -taskList Test -parameters @{"solutionFile" = "D:\Projects\STR\GIT\STR-Refresh\src\SelecT.DMS\SelecT.DMS.sln"}