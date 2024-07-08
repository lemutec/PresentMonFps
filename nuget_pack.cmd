@REM rd /s /q C:\Users\{UserName}\.nuget\packages\PresentMonFps
@REM dotnet nuget locals all --clear
cd pack
dotnet restore
dotnet pack PresentMonFps.csproj -c Release -o ../
@pause
