REM Config
set NGINX_DIR=nginx
set THISDIR=%~dp0

REM Copy resources
rmdir /s /q .\res

mkdir .\res
mkdir .\res\A2BBIdSrv
mkdir .\res\A2BBAPI

xcopy /y /s ..\A2BBIdentityServer\bin\Debug\netcoreapp1.1 .\res\A2BBIdSrv
copy /y ..\A2BBIdentityServer\appsettings.json .\res\A2BBIdSrv\appsettings.json
xcopy /y /s ..\A2BBAPI\bin\Debug\netcoreapp1.1 .\res\A2BBAPI
copy /y ..\A2BBAPI\appsettings.json .\res\A2BBAPI\appsettings.json

REM Start DotNet
cd .\res\A2BBIdSrv
start dotnet .\A2BBIdentityServer.dll
cd ..\..\res\A2BBAPI
start dotnet .\A2BBAPI.dll
cd ..\..

REM Start nginx
copy /y .\nginx.conf %NGINX_DIR%\conf\nginx.conf
%NGINX_DIR%\nginx.exe -p %NGINX_DIR%
