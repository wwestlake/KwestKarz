function combine-cs-files {
    param (
        [string]$RootPath = ".",
        [string]$OutputFile = "combined_output.txt"
    )

    $absoluteRoot = Resolve-Path $RootPath
    $csFiles = Get-ChildItem -Path $absoluteRoot -Recurse -Filter *.cs -File

    if (Test-Path $OutputFile) {
        Remove-Item $OutputFile
    }

    foreach ($file in $csFiles) {
        $absolutePath = $file.FullName
        Add-Content -Path $OutputFile -Value "// $absolutePath"
        Get-Content -Path $absolutePath | Add-Content -Path $OutputFile
        Add-Content -Path $OutputFile -Value "`n"
    }

    Write-Host "Combined all .cs files from $absoluteRoot into $OutputFile"
}

function combine-js-files {
    param (
        [string]$RootPath = ".",
        [string]$OutputFile = "combined_output.txt"
    )

    $absoluteRoot = Resolve-Path $RootPath
    $jsFiles = Get-ChildItem -Path $absoluteRoot -Recurse -Include *.js, *.jsx -File | Where-Object {
        $_.FullName -notmatch '\\node_modules\\' -and $_.FullName -notmatch '\\dist\\'
    }

    if (Test-Path $OutputFile) {
        Remove-Item $OutputFile
    }

    foreach ($file in $jsFiles) {
        $absolutePath = $file.FullName
        Add-Content -Path $OutputFile -Value "// $absolutePath"
        Get-Content -Path $absolutePath | Add-Content -Path $OutputFile
        Add-Content -Path $OutputFile -Value "`n"
    }

    Write-Host "Combined all .js and .jsx files from $absoluteRoot into $OutputFile"
}
