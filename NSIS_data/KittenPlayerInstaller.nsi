Outfile "installer.exe"
!include "LogicLib.nsh"
!include "WinVer.nsh"
!include "MUI2.nsh"
!include "x64.nsh"
!include "VersionCompare.nsh"
!include "ZipDLL.nsh"

RequestExecutionLevel admin

Name "Kitten Player"
InstallDir "$PROGRAMFILES\Kitten Player"

VIAddVersionKey ProductName "Kitten Player"
VIAddVersionKey CompanyName "Ch3shireDev Studios"
VIProductVersion "0.0.1.1"

!define MUI_ICON "..\publish\Application Files\KittenPlayer_0_0_1_1\Resources\kitteh.ico"
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
	Push "4.0.30319"
	Call VersionCompare
	Pop $R0     ; $R0="1"

  IntCmp $R0 1 HigherVersion EqualVersion LowerVersion
  
  HigherVersion:
    Goto installCodec

  EqualVersion:
    Goto installUpdate

  LowerVersion:
    Goto InstallDotNet


  installDotNet:
  SetDetailsView show

  StrCpy $0 "https://download.microsoft.com/download/1/B/E/1BE39E79-7E39-46A3-96FF-047F95396215/dotNetFx40_Full_setup.exe"
  inetc::get $0 "$TEMP\dotnet.exe"
  Pop $0
  DetailPrint "dotNET 4.0 download: $0"
  SetDetailsView show
  ExecWait "$TEMP\dotnet.exe"

  installUpdate:
    MessageBox MB_YESNO "Install .NET 4.0.3 update? It's required for proper functionality of Kitten Player." /SD IDYES IDNO installCodec

  ${If} ${RunningX64}
      StrCpy $0 "https://download.microsoft.com/download/2/B/F/2BF4D7D1-E781-4EE0-9E4F-FDD44A2F8934/NDP40-KB2468871-v2-x64.exe"
  ${Else}
      StrCpy $0 "https://download.microsoft.com/download/3/3/9/3396A3CA-BFE8-4C9B-83D3-CADAE72C17BE/NDP40-KB2600211-x86.exe"
  ${EndIf}  

  inetc::get $0 "$TEMP\dotnetPatch.exe"
  Pop $1
  DetailPrint "dotNET 4.0.3 download: $1"
  SetDetailsView show
  ExecWait "$TEMP\dotnetPatch.exe"

installCodec:    

${If} ${IsWin10}
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
File /r "..\publish\Application Files\KittenPlayer_0_0_1_1\*"
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

Var ffmpeg

Function DownloadFFmpeg
StrCpy $ffmpeg "ffmpeg-3.4-win32-static"
StrCpy $0 "http://ffmpeg.zeranoe.com/builds/win32/static/$ffmpeg.zip"
inetc::get $0 "$TEMP\ffmpeg.zip"
ZipDLL::extractfile "$TEMP\ffmpeg.zip" "$TEMP" "$ffmpeg\bin\ffmpeg.exe"
CopyFiles "$TEMP\$ffmpeg\bin\ffmpeg.exe" "$INSTDIR\ffmpeg.exe"
FunctionEnd

Section

Call InstallFunction

SectionEnd

Section

Call DownloadYoutubeDL
Call DownloadFFmpeg

SectionEnd
