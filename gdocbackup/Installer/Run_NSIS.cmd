@echo off

del .\GDocBackup_*.exe   /Q
del .\CompiledBIN\   /Q

xcopy ..\GDocBackup\bin\Release\GDocBackup.exe                  .\CompiledBIN\  /Y
xcopy ..\GDocBackup\bin\Release\GDocBackupLib.dll               .\CompiledBIN\  /Y
rem xcopy ..\GDocBackup\bin\Release\Google.GData.AccessControl.dll  .\CompiledBIN\  /Y
xcopy ..\GDocBackup\bin\Release\Google.GData.Client.dll         .\CompiledBIN\  /Y
xcopy ..\GDocBackup\bin\Release\Google.GData.Documents.dll      .\CompiledBIN\  /Y
xcopy ..\GDocBackup\bin\Release\Google.GData.Extensions.dll     .\CompiledBIN\  /Y



PAUSE


"c:\Program Files\NSIS\makensis.exe" Installer.nsi

PAUSE
