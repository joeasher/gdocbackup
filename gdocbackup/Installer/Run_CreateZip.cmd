@echo --- CREATING ZIP FILES ---

DEL  GDocBackup_$$$VerMajor$$$.$$$VerMinor$$$.$$$VerBuild$$$.$$$VerRevision$$$_BIN.zip
DEL  GDocBackup_$$$VerMajor$$$.$$$VerMinor$$$.$$$VerBuild$$$.$$$VerRevision$$$_CMD.zip 

"%PROGRAMFILES(x86)%\7-Zip\7z.exe"    a   GDocBackup_$$$VerMajor$$$.$$$VerMinor$$$.$$$VerBuild$$$.$$$VerRevision$$$_BIN.zip     .\CompiledBIN\*.exe   .\CompiledBIN\*.dll
"%PROGRAMFILES(x86)%\7-Zip\7z.exe"    a   GDocBackup_$$$VerMajor$$$.$$$VerMinor$$$.$$$VerBuild$$$.$$$VerRevision$$$_CMD.zip     .\CompiledBIN_CMD\*.exe   .\CompiledBIN_CMD\*.dll

