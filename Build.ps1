function Coalesce($a, $b) { if ($a -ne $null) { $a } else { $b } }

echo "build: Build started"

Push-Location $PSScriptRoot

if(Test-Path .\artifacts) {
	echo "build: Cleaning .\artifacts"
	Remove-Item .\artifacts -Force -Recurse
}

Push-Location "test\Serilog.Settings.Comparison.Tests"

& dotnet xunit -xml ../../artifacts/testOutput.xml

Pop-Location

Copy-Item .\assets\pre-docs.md .\artifacts\README.md -Force

Push-Location "artifacts"

[xml]$xml = Get-Content testOutput.xml
$xml.SelectNodes('/assemblies/assembly/collection') | Sort-Object name | ForEach-Object {
    New-Object -Type PSObject -Property @{
        Description = $_.name.Substring(3).Replace("\r\n", "`n")
        Tests = $_.SelectNodes('test') | ForEach-Object {
                    if ($_.result -eq "Skip") { ":warning: $($_.reason.InnerText)`n" }
                    elseif ($_.result -eq "Fail") { "$($_.output.InnerText)`n:heavy_exclamation_mark: **Test Failed** : `n``````$($_.failure.message.InnerText.Replace("\r\n", "`n"))`n``````" }
                    else { $_.output.InnerText }
                } 
    }
} | % {@($_.Description) + "`n" + $_.Tests} | Out-File README.md -Encoding utf8 -Append
Pop-Location

Copy-Item artifacts/*.md docs

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
