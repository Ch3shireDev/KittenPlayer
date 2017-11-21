# define the name of the installer
Outfile "installer.exe"
!include "MUI2.nsh"
 
# define the directory to install to, the desktop in this case as specified  
# by the predefined $DESKTOP variable

RequestExecutionLevel admin

Name "Kitten Player"
InstallDir "$PROGRAMFILES\Kitten Player"

!define MUI_ICON "pure\Resources\kitteh.ico"
!insertmacro MUI_PAGE_WELCOME
ShowInstDetails show
Page directory
Page instfiles PreparationFunction InstallFunction AfterInstallationFunction /ENABLECANCEL
# !define MUI_FINISHPAGE_TITLE '${FINISH_TITLE}'
!define MUI_FINISHPAGE_TITLE_3LINES
!define MUI_FINISHPAGE_RUN_FUNCTION finishpageaction
!insertmacro MUI_PAGE_FINISH
# !define MUI_FINISHPAGE_RUN_FUNCTION finishpageaction
# !define MUI_FINISHPAGE_RUN "$instdir\KittenPlayer.exe"

!insertmacro MUI_LANGUAGE "English"

Function .onInit
FunctionEnd

Function InstallFunction
SetOutPath "$INSTDIR"
File /r "pure\*"
# Call DownloadYoutubeDL
CreateShortcut "$desktop\Kitten Player.lnk" "$instdir\KittenPlayer.exe"
FunctionEnd

Function PreparationFunction
# MessageBox MB_OK "FunB"
FunctionEnd

Function AfterInstallationFunction
Call DownloadYoutubeDL
FunctionEnd

Function finishpageaction
FunctionEnd

Function DownloadYoutubeDL
  StrCpy $0 "https://youtube-dl.org/downloads/latest/youtube-dl.exe"
#   inetc::get $0 "$INSTDIR\youtube-dl.exe"
FunctionEnd

Section
# MessageBox MB_OK "Sections" 
SectionEnd