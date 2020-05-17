if exist ".\TestResults" rd /q /s ".\TestResults"

dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings