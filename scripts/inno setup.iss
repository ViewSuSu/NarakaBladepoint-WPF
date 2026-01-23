#define MyAppName "NarakaBladepoint-WPF"
#define MyAppExe "NarakaBladepoint.App.exe"
#define MyAppBinPath "..\NarakaBladepoint.App\bin\Release\net6-windows"
#define MyAppVersion GetFileVersion(MyAppBinPath + "\NarakaBladepoint.App.dll")
#define MyAppPublisher GetStringFileInfo(MyAppBinPath + "\NarakaBladepoint.App.dll", "CompanyName")
#define MyAppURL "https://github.com/ViewSuSu/NarakaBladepoint-WPF"


[Setup]
AppId={{5D0E8B1A-7A9B-4B7C-9F8D-5F6D2C3B4A4E}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
DefaultDirName=D:\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputDir=.
OutputBaseFilename={#MyAppName}
SetupIconFile=Icon.ico
WizardSmallImageFile=Icon.bmp
Compression=lzma
SolidCompression=yes
WizardStyle=modern
UninstallDisplayName={#MyAppName}
UninstallDisplayIcon={app}\Icon.ico


[Languages]
Name: "chinesesimplified"; MessagesFile: "compiler:Languages\ChineseSimplified.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkedonce 

[Files]
Source: "{#MyAppBinPath}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "Icon.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "Icon.bmp"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExe}"; IconFilename: "{app}\Icon.ico"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"; IconFilename: "{app}\Icon.ico"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExe}"; IconFilename: "{app}\Icon.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExe}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

