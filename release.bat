dotnet publish -p:IncludeNativeLibrariesForSelfExtract=true
copy cli\publish\cli.exe pbi.exe
for /f "delims=" %%i in ('powershell -Command "& {[System.Reflection.Assembly]::LoadFrom(\"cli\bin\Debug\net5.0\win-x64\cli.dll\").GetName().Version.ToString()}"') do set output=%%i
rem gh release create %output% --title "%output%" --notes "@gatapia"