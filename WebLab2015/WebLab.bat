@echo off
set currdate=%date%
set newdate=%currdate:/=-%
set newdate=%newdate:\=-%
echo %newdate%
@echo on
del E:\dotnetproject\Swati\PROJECTS\WebLab2015\WebLab2015.zip
E:\dotnetproject\Swati\PROJECTS\WebLab2015\WinRAR.exe -afzip a  WebLab2015.zip -ep1 -r E:\dotnetproject\Swati\PROJECTS\WebLab2015\WebLab  -x*\*.cs  -x*\*.bmp  -x*\*.mdb  -x*\*.rar  -x*\*.config -x*\*.pdb -x*\*.zip -x*\*.txt -x*\*.xml -x*\*.htm -x*\*.html -x*\*.refresh  -x*\*.*1 -x*\*.*2 -x*\*.*3 -x*\*.*4  -x*\*.*5  -x*\*.*6  -x*\*.*7 -x*\*.*8 -x*\*.*9 -x*\*.*0   -x*\*.vss  -x*\*.svclog  -x*\*.scc -xCVS\* -x*\*.user -x*\*.tmp -x*\*.csproj -x*\*.EMF -x*\*.XLS -x*\*.vspscc  -x*\comp_logo.jpeg -x*\comp_logo.jpg -x*\EcapsMainPage.htm  -x*\MaxSessCount.txt -x*\myLogin.html -x*\logo-Hospital.jpg -x*\loginid.jpg

