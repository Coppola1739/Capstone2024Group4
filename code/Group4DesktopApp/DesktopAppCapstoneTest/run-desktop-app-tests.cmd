dotnet test --collect:"XPlat Code Coverage"
"%UserProfile%\.nuget\packages\reportgenerator\5.2.0\tools\net8.0\ReportGenerator.exe" -reports:./TestResults\*\coverage.cobertura.xml -targetdir:coveragereport -filefilters:-*\View*;-*\UserControls*;-*\Model\AlertDialog.cs 
cd coveragereport || { echo "Error: 'coveragereport' folder not found"; exit 1; }
REM Open the "index.html" file in the default browser
start index.html