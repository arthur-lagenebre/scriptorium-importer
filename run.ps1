Get-ChildItem -Recurse -Filter *.csproj | Where-Object { $_.FullName -notmatch '\\(bin|obj)\\' } | ForEach-Object {
    $c = (Get-Content $_.FullName -Raw).Replace('<TargetFramework>net8.0</TargetFramework>','<TargetFramework>net10.0</TargetFramework>')
    [System.IO.File]::WriteAllText($_.FullName, $c, (New-Object System.Text.UTF8Encoding($false)))
}