Include ".\helpers.ps1"

properties{
	#$testMessage = 'Executed Test!!'
	$compileMessage = 'Executed Compile!!'
	$cleanMessage = 'Executed Clean!!'

	$solutionDirectory=(Get-Item $solutionFile).DirectoryName
	$outputDirectory="$solutionDirectory\.build"
	$tempDirectory="$outputDirectory\package"
	$publishedWebsitesDirectory = "$tempDirectory\_PublishedWebsites"

	$buildConfiguration ="Release"
	$buildPlatform ="Any CPU"

	$packagesPath = "$solutionDirectory\packages"
	$7ZipExe = (Find-PackagePath $packagesPath "7-Zip.CommandLine" ) + "\tools\7za.exe"
	#$7ZipExe = "C:\Users\sanju.khattar\Documents\Visual Studio 2013\Projects\WpfApplication1\packages\7-Zip.CommandLine.9.20.0\tools\7za.exe"	
}

FormatTaskName "`r`n`r`n------Executing {0} Task -----------"

task default -depends Test

task Backup -description "This task will Backup the "{
	Write-Host $7ZipExe
	Write-Host "Backup the earlier Build directory"
	Assert (Test-Path $7ZipExe) "7-Zip Command Line could not be found $7ZipExe"

	if(Test-Path $outputDirectory)
	{
		$curDate = Get-Date
		$strDateDay= $curDate.Day 
		$strDateMonth= $curDate.Month 
		$strDateYear= $curDate.Year 
		$strDateHour= $curDate.Hour 
		$strDateMinute= $curDate.Minute 
		$strDateSecond= $curDate.Second
		$curDateStr = "$strDateDay$strDateMonth$strDateYear$strDateHour$strDateMinute$strDateSecond"
		#Write-Host $curDateStr
		$inputDirectory = "$outputDirectory\*"
		$archivePath = "$solutionDirectory\build-backup$curDateStr.zip"
		Exec { & $7ZipExe a -r -mx3 $archivePath $inputDirectory }
	}
}

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

task Compile -depends Backup,Init -description "Compile the task" -requiredVariables solutionFile,buildConfiguration,buildPlatform,tempDirectory {
	Write-Host "Building Solution $solutionDirectory"
	Exec { msbuild $solutionFile "/p:Configuration=$buildConfiguration;Platform=$buildPlatform;OutDir=$tempDirectory" }
}

task Test -depends Compile, Clean -description "This is test task" {
	Write-Host $testMessage
}