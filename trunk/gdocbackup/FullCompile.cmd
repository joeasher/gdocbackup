@echo *******************************************************************
@echo  FULL COMPILE
@echo *******************************************************************
@echo .
@echo .


subwcrev.exe . GDocBackupVersion.template.txt GDocBackupVersion.cs

pause


CALL "%VS80COMNTOOLS%..\..\VC\vcvarsall.bat" x86

MSBuild  GDocBackup.sln  /t:Clean,Rebuild /p:Configuration=Release


@echo *************** END *************** 

pause

