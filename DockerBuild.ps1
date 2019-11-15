$extraArgs = "";

$extraArgs += " --env NUGET_PACKAGES=/nuget"
$extraArgs += " --rm"
$extraArgs += " --volume /var/run/docker.sock:/var/run/docker.sock"
$extraArgs += " --volume ${PWD}:/build"
$extraArgs += " --workdir /build"

if($env:TEAMCITY_BUILD_PROPERTIES_FILE)
{
    Write-Output "Copy $($env:TEAMCITY_BUILD_PROPERTIES_FILE)"
	$props = ConvertFrom-StringData (Get-Content $env:TEAMCITY_BUILD_PROPERTIES_FILE  -raw)

	Write-Output $props

    Write-Output "Copy $($props["teamcity.configuration.properties.file"])"
    Copy-Item -Path $props["teamcity.configuration.properties.file"] -Destination "teamcity.configuration.properties"

	Write-Output "Copy $($props["teamcity.runner.properties.file"] )"
    Copy-Item -Path $props["teamcity.runner.properties.file"] -Destination "teamcity.runner.properties"

    $props["teamcity.build.properties.file"] = "/build/teamcity.build.properties"
    $props["teamcity.configuration.properties.file"] = "/build/teamcity.configuration.properties"
    $props["teamcity.runner.properties.file"] = "/build/teamcity.runner.properties"

    Set-Content "teamcity.build.properties" -Value ($props.GetEnumerator() | % { "$($_.Name)=$($_.Value)" })

	$extraArgs += " -i"
	$extraArgs += " --env TEAMCITY_BUILD_PROPERTIES_FILE=/build/teamcity.build.properties"
	$extraArgs += " --env TEAMCITY.VERSION='2019.1.2 (build 66342)'"
	$extraArgs += " -v /nuget:/nuget"
}
else
{
	#$extraArgs += " --env TEAMCITY_VERSION='2019.1.2 (build 66342)'"
    $extraArgs += " --tty"
	$extraArgs += " -it"
	$extraArgs += " -v D:/NuGet:/nuget"
}

docker build -t buildcontainer ./Docker/Build

#$command = "docker run ${extraArgs} buildcontainer"
$command = "docker run ${extraArgs} buildcontainer ./build.sh ${args}"

Write-Host $command

iex $command

