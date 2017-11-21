!include "MUI2.nsh"
# !include "inetc.nsh"

Name "Kitten Player"
OutFile "KittenPlayerInstaller.exe"

InstallDir "$PROGRAMFILES\Kitten Player"

RequestExecutionLevel user

#   !define MUI_ABORTWARNING

# !insertmacro MUI_PAGE_LICENSE "${NSISDIR}\Docs\Modern UI\License.txt"
# !insertmacro MUI_PAGE_COMPONENTS
# !insertmacro MUI_PAGE_DIRECTORY
# !insertmacro MUI_PAGE_INSTFILES
# !insertmacro MUI_PAGE_FINISH

# !insertmacro MUI_UNPAGE_WELCOME
# !insertmacro MUI_UNPAGE_CONFIRM
# !insertmacro MUI_UNPAGE_INSTFILES
# !insertmacro MUI_UNPAGE_FINISH

# !define MUI_WELCOMEPAGE_TEXT "Sample text"
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_LANGUAGE "English"
# Page custom FunA "caption"
Page custom FunA FunB
!insertmacro MUI_PAGE_COMPONENTS

Var install
Var version

Var i

Function FunA
ReadRegStr $install HKEY_LOCAL_MACHINE "Software\Microsoft\NET Framework Setup\NDP\v4\Client" "Install"
ReadRegStr $version HKEY_LOCAL_MACHINE "Software\Microsoft\NET Framework Setup\NDP\v4\Client" "Version"

MessageBox MB_OK $install
MessageBox MB_OK $version

# NSISdl::download http://download.nullsoft.com/winamp/client/winamp291_lite.exe "X.exe"
inetc::put "http://dl.zvuki.ru/6306/mp3/12.mp3" "$EXEDIR\12.mp3"
Pop $0
MessageBox MB_OK "Download complete"
FunctionEnd

Function FunB
MessageBox MB_OK "FunA"
FunctionEnd

Section
ReadRegStr $install HKEY_LOCAL_MACHINE "Software\Microsoft\NET Framework Setup\NDP\v4\Client" "Install"
ReadRegStr $version HKEY_LOCAL_MACHINE "Software\Microsoft\NET Framework Setup\NDP\v4\Client" "Version"

StrCpy $version "4.0.1"
StrCpy $install "0"


MessageBox MB_OK "$install"

IntCmp $install 1 legit notlegit

legit:
MessageBox MB_OK "OK"
goto version
notlegit:
MessageBox MB_OK "Not OK"
version:
MessageBox MB_OK "$version"

    # SetOutPath "$INSTDIR"
    # WriteRegStr HKCU "Software\Modern UI Test" "" $INSTDIR

SectionEnd


LangString DESC_SecDummy ${LANG_ENGLISH} "A test section."

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
!insertmacro MUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
!insertmacro MUI_FUNCTION_DESCRIPTION_END
