!include "LogicLib.nsh"
!include "WinVer.nsh"
!include "MUI2.nsh"
!include "x64.nsh"
!include "VersionCompare.nsh"
!include "ZipDLL.nsh"

# RequestExecutionLevel admin

Name "Kitten Player"
Outfile "KittenPlayer-v0.0.2.0-installer.exe"
InstallDir "$PROGRAMFILES\Kitten Player"

VIAddVersionKey ProductName "Kitten Player"
VIAddVersionKey CompanyName "Ch3shireDev Studios"
VIProductVersion "0.0.2.0"

!define MUI_ICON "..\publish\Application Files\KittenPlayer_0_0_2_0\Resources\kitteh.ico"
!insertmacro MUI_PAGE_WELCOME
ShowInstDetails show
Page directory
# Page instfiles nullfunc DownloadDotNET
!insertmacro MUI_PAGE_INSTFILES
!define MUI_FINISHPAGE_TITLE_3LINES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_LANGUAGE "English"

Function .onInit
FunctionEnd

Function nullfunc
FunctionEnd

Function DownloadDotNET
  
  ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" Version
  # ${VersionCheck} "$0" "4.0.30319" "$result"
  Push "$0"
	Push "4.5.0"
	Call VersionCompare
	Pop $R0     ; $R0="1"

  IntCmp $R0 1 HigherVersion EqualVersion LowerVersion
  
  HigherVersion:
    Goto installCodec

  EqualVersion:
    Goto HigherVersion

  LowerVersion:
    Goto InstallDotNet


  installDotNet:
  SetDetailsView show

  StrCpy $0 "https://download.microsoft.com/download/B/4/1/B4119C11-0423-477B-80EE-7A474314B347/NDP452-KB2901954-Web.exe"
  inetc::get $0 "$TEMP\dotnet.exe"
  Pop $0
  DetailPrint "dotNET 4.5.2 download: $0"
  SetDetailsView show
  ExecWait "$TEMP\dotnet.exe"

installCodec:    

${If} ${IsWin10}
Goto done
${ElseIf} ${${IsWin8.1}}
Goto done
${EndIf}

MessageBox MB_YESNO "Install XCodecPack? Codecs are requied to play mpeg format used by YouTube." /SD IDYES IDNO done

  Call DownloadCodec

done:

FunctionEnd

Function DownloadCodec
  StrCpy $0 "http://www.xpcodecpack.com/dl/X-Codec-Pack_2.7.4.exe"
  inetc::get $0 "$TEMP\codec.exe"
  Pop $1
  
  DetailPrint "MediaPlayerCodecPack download: $1"
  ExecWait "$TEMP\codec.exe"
FunctionEnd

Function InstallFunction
SetOutPath "$INSTDIR"
File /r "..\publish\Application Files\KittenPlayer_0_0_2_0\*"
File "C:\Windows\System32\msvcr100.dll"
CreateShortcut "$desktop\Kitten Player.lnk" "$instdir\KittenPlayer.exe"
FunctionEnd

Function PreparationFunction
FunctionEnd

Function AfterInstallationFunction
FunctionEnd

Function DownloadYoutubeDL
  StrCpy $0 "https://youtube-dl.org/downloads/latest/youtube-dl.exe"
  inetc::get $0 "$INSTDIR\youtube-dl.exe"

FunctionEnd

Section

Call InstallFunction

SectionEnd

Section
Call DownloadYoutubeDL
# Call DownloadFFmpeg
SectionEnd
