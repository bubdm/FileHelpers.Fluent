Param(
    [Parameter(Mandatory=$true)][string]$apiKey
)

$packages = Get-ChildItem -Filter *.nupkg -Recurse -File -Name | ForEach-Object { [System.IO.Path]::GetFullPath($_) }

ForEach ($package in $packages) {
    Write-Host $package
    d:\a\1\s\tools\nuget.exe push -SkipDuplicate -ApiKey $apiKey -Source https://api.nuget.org/v3/index.json $package
}