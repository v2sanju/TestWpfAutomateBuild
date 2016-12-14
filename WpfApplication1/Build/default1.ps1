properties{
	$testMessage = 'Executed Test!!'
	$compileMessage = 'Executed Compile!!'
	$cleanMessage = 'Executed Clean!!'

	$solutionDirectory=(Get-Item $solutionFile).DirectoryName
	$outputDirectory="$solutionDirectory\.build"
	$tempDirectory="$outputDirectory\temp"

	$buildConfiguration ="Release"
	$buildPlatform ="Any CPU"
}

FormatTaskName "`r`n`r`n------Executing {0} Task -----------"

task default -depends Test

task Init -description "Init the build by removing previous artifacts and creating out directories"{
	Write-Host "Creating output directory located at ..\.build"
	if(Test-Path $outputDirectory)
	{
		Remove-Item $outputDirectory -Force -Recurse
	}

	New-Item $outputDirectory -ItemType Directory | Out-Null
	New-Item $tempDirectory -ItemType Directory | Out-Null
}

task Clean -description "Remove temporary files" {
	Write-Host $cleanMessage
}

task Compile -depends Init -description "Compile the task" -requiredVariables solutionFile,buildConfiguration,buildPlatform,tempDirectory {
	Write-Host "Building Solution $solutionDirectory"
	Exec { msbuild $solutionFile "/p:Configuration=$buildConfiguration;Platform=$buildPlatform;OutDir=$tempDirectory" }
}

task Test -depends Compile, Clean -description "This is test task" {
	Write-Host $testMessage
}