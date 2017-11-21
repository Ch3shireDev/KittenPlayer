Name "dotNET install"
OutFile "dotnet.exe"
RequestExecutionLevel admin

!include "LogicLib.nsh"
!include "WinVer.nsh"
!include "MUI2.nsh"
!include "x64.nsh"
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install-colorful.ico"
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_LANGUAGE "English"

# Page custom FunA "caption"
# Page custom FunA FunB
# !insertmacro MUI_PAGE_COMPONENTS

# Section
#   DetailPrint "dotNET installation"
#   SetDetailsView show
# # Call DownloadDotNET
# SectionEnd

# !insertmacro MUI_PAGE_INSTFILES

Page custom customFun customFun

Section
  DetailPrint "codec installation"
  SetDetailsView show
SectionEnd

Function customFun
  DetailPrint "ciasteczka"
  SetDetailsView show
FunctionEnd

Function DownloadDotNET
  StrCpy $0 "https://download.microsoft.com/download/1/B/E/1BE39E79-7E39-46A3-96FF-047F95396215/dotNetFx40_Full_setup.exe"
  inetc::get $0 "$TEMP\dotnet.exe"
  Pop $0
  DetailPrint "dotNET 4.0 download: $0"
  ExecWait "$TEMP\dotnet.exe"

  ${If} ${RunningX64}
      StrCpy $0 "https://download.microsoft.com/download/2/B/F/2BF4D7D1-E781-4EE0-9E4F-FDD44A2F8934/NDP40-KB2468871-v2-x64.exe"
  ${Else}
      StrCpy $0 "https://download.microsoft.com/download/3/3/9/3396A3CA-BFE8-4C9B-83D3-CADAE72C17BE/NDP40-KB2600211-x86.exe"
  ${EndIf}  

  inetc::get $0 "$TEMP\dotnetPatch.exe"
  Pop $1
  DetailPrint "dotNET 4.0.3 download: $1"
  ExecWait "$TEMP\dotnetPatch.exe"
  
  SetDetailsView show
FunctionEnd

Function DownloadCodec
  ${If} ${IsWinXP}
  StrCpy $0 "http://download.mediaplayercodecpack.com/files/media.player.codec.pack.v4.4.7.setup.legacy.exe"
  ${Else}
  StrCpy $0 "http://download.mediaplayercodecpack.com/files/media.player.codec.pack.v4.4.7.setup.exe"
  ${Endif}
  inetc::get $0 "$TEMP\codec.exe"
  Pop $1
  
  DetailPrint "MediaPlayerCodecPack download: $1"
  ExecWait "$TEMP\codec.exe"
FunctionEnd