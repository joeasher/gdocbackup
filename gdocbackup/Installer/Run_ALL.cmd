@echo off

CLS

@echo --- Delete temp files ---
del .\GDocBackup_*.exe   /Q
del .\CompiledBIN\   /Q
del .\CompiledBIN_CMD\   /Q
del Installer.runme.nsi


rem //////////////////////////////////////

@echo ----------------------

xcopy ..\GDocBackup\bin\Release\GDocBackup.exe                  .\CompiledBIN\  /Y
xcopy ..\GDocBackup\bin\Release\GDocBackupLib.dll               .\CompiledBIN\  /Y
xcopy ..\GDocBackup\bin\Release\Google.GData.Client.dll         .\CompiledBIN\  /Y
xcopy ..\GDocBackup\bin\Release\Google.GData.Documents.dll      .\CompiledBIN\  /Y
xcopy ..\GDocBackup\bin\Release\Google.GData.Extensions.dll     .\CompiledBIN\  /Y
xcopy ..\GDocBackup\bin\Release\Google.GData.AccessControl.dll  .\CompiledBIN\  /Y

@echo ----------------------

xcopy ..\GDocBackupCMD\bin\Release\GDocBackupCMD.exe               .\CompiledBIN_CMD\  /Y
xcopy ..\GDocBackupCMD\bin\Release\GDocBackupLib.dll               .\CompiledBIN_CMD\  /Y
xcopy ..\GDocBackupCMD\bin\Release\Google.GData.Client.dll         .\CompiledBIN_CMD\  /Y
xcopy ..\GDocBackupCMD\bin\Release\Google.GData.Documents.dll      .\CompiledBIN_CMD\  /Y
xcopy ..\GDocBackupCMD\bin\Release\Google.GData.Extensions.dll     .\CompiledBIN_CMD\  /Y
xcopy ..\GDocBackupCMD\bin\Release\Google.GData.AccessControl.dll  .\CompiledBIN_CMD\  /Y

@echo ----------------------

PAUSE

rem //////////////////////////////////////

AxInfoSR.exe .\CompiledBIN\GDocBackup.exe  Installer.nsi  Installer.runme.nsi
"%PROGRAMFILES%\NSIS\makensis.exe" Installer.runme.nsi
DEL Installer.runme.nsi /Q

rem //////////////////////////////////////

AxInfoSR.exe .\CompiledBIN\GDocBackup.exe  Run_CreateZip.cmd  Run_CreateZip.runme.cmd
CALL Run_CreateZip.runme.cmd
DEL Run_CreateZip.runme.cmd /Q

rem //////////////////////////////////////

@echo .
@echo .
@echo ------------ END ------------
@echo .

PAUSE 