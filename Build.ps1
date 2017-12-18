echo "build: Build started"

Push-Location $PSScriptRoot

if(Test-Path .\artifacts) {
	echo "build: Cleaning .\artifacts"
	Remove-Item .\artifacts -Force -Recurse
}

Push-Location "test\Serilog.Settings.Comparison.Tests"

& dotnet xunit -xml ../../artefacts/testOutput.xml

Pop-Location

Push-Location "artefacts"
[xml]$xml = Get-Content testOutput.xml
$xml.SelectNodes('/assemblies/assembly/collection') | Sort-Object name | ForEach-Object {
    New-Object -Type PSObject -Property @{
        Description = $_.name.Substring(3).Replace("\r\n", "`n")
        Tests = $_.SelectNodes('test') | ForEach-Object {
                    $_.output.InnerText
                } 
    }
} | % {@($_.Description) + "`n" + $_.Tests} | Out-File README.md -Encoding utf8 -Force
Pop-Location

Copy-Item artefacts/*.md docs

Push-Location "docs"
Get-ChildItem * -Include *.md | ForEach-Object {
    ## If contains UNIX line endings, replace with Windows line endings
    if (Get-Content $_.FullName -Delimiter "`0" | Select-String "[^`r]`n")
    {
        $content = Get-Content $_.FullName
        $content | Set-Content $_.FullName
    }
}
Pop-Location

Pop-Location
