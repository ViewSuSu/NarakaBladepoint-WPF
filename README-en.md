![.NET 6](https://img.shields.io/badge/.NET%206-Windows%20Desktop-purple)
![Platform](https://img.shields.io/badge/Platform-WPF-orange)
![License](https://img.shields.io/badge/license-MIT-lightgrey)

[English](README-en.md) | [简体中文](README.md)

[![Download Latest Installer](https://img.shields.io/badge/Download_Latest_Installer-NarakaBladepoint--WPF.exe-blue?style=for-the-badge&logo=windows)](https://github.com/ViewSuSu/NarakaBladepoint-WPF/releases/latest/download/NarakaBladepoint-WPF.exe)

> [!TIP]
> If it fails to run, ensure [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) or later is installed.



# Naraka Bladepoint Client (WPF)


## <span style="color: #0969da;">About This Project</span>

- <span style="color: #238636;">**Motivation**</span> <span style="color: #6e7681;">- Played 2000+ hours of Naraka Bladepoint and recently realized how brilliantly designed the client's UX/UI is (props to the big-name product managers!), so I wanted to try recreating that Unity feel with WPF</span>
- <span style="color: #238636;">**Tech Stack**</span> <span style="color: #6e7681;">- .NET 6, all custom controls, no third-party UI libs</span>
- <span style="color: #238636;">**Architecture**</span> <span style="color: #6e7681;">- Meticulously designed, following strict MVVM patterns and WPF best practices, elegant code structure</span>
- <span style="color: #238636;">**AI**</span> <span style="color: #6e7681;">- Later discovered VibeCoding was amazing for bulk code generation - total game changer!</span>
- <span style="color: #238636;">**Code Quality**</span> <span style="color: #6e7681;">- Project is half human, half AI-assisted</span>

<div align="center">
  <img src="./docs/MainWindows.png" alt="Main Window" width="100%" />
  <br/>
  <em>Main Interface</em>
</div>

## <span style="color: #0969da;">What You Can Learn From This</span>

If you're working with WPF, this project might be useful:

- <span style="color: #238636;">**Game-level UI implementation**</span> <span style="color: #6e7681;">- Complex layouts, animations, interactions - all the real problems and solutions</span>
- <span style="color: #238636;">**Prism in practice**</span> <span style="color: #6e7681;">- How to split a large project into modules that work independently yet together</span>
- <span style="color: #238636;">**Building controls from scratch**</span> <span style="color: #6e7681;">- No third-party libraries, all custom controls written by hand, you can see all the details</span>
- <span style="color: #238636;">**Data binding tricks**</span> <span style="color: #6e7681;">- How to handle data flow elegantly in MVVM</span>
- <span style="color: #238636;">**Performance optimization**</span> <span style="color: #6e7681;">- Performance issues you'll face with complex UIs and how to solve them</span>
- <span style="color: #238636;">**Project architecture**</span> <span style="color: #6e7681;">- How dependency injection, base class design, and module decoupling actually work in real projects</span>

Code is all open source. Some parts could be better - feel free to open issues or PRs.

## <span style="color: #0969da;">More Screenshots</span>

<table align="center" width="100%">
  <tr>
    <td align="center" width="33.33%">
      <img src="./docs/HeroList.png" alt="Hero List" width="100%" /><br/>
      <em>Hero List</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/IllustratedCollection.png" alt="Illustrated Collection" width="100%" /><br/>
      <em>Illustrated Collection</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/PersonalInfo.png" alt="Personal Info" width="100%" /><br/>
      <em>Personal Info</em>
    </td>
  </tr>
  <tr>
    <td align="center" width="33.33%">
      <img src="./docs/TippingRecord.png" alt="Tipping Record" width="100%" /><br/>
      <em>Tipping Record</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/AvatarList.png" alt="Avatar List" width="100%" /><br/>
      <em>Avatar List</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/HistoryData.png" alt="History Data" width="100%" /><br/>
      <em>History Data</em>
    </td>
  </tr>
  <tr>
    <td align="center" width="33.33%">
      <img src="./docs/SocialTag.png" alt="Social Tag" width="100%" /><br/>
      <em>Social Tag</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/Tag.png" alt="Tag" width="100%" /><br/>
      <em>Tag</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/Weapon.png" alt="Weapon" width="100%" /><br/>
      <em>Weapon</em>
    </td>
  </tr>
</table>


## <span style="color: #0969da;">Project Architecture</span>

Built with **Prism** for modular layers. Here's the basic structure:

```mermaid
graph LR
    App["<b>App</b><br/>Entry Point"]
    Modules["<b>Modules</b><br/>Business Logic"]
    Shared["<b>Shared</b><br/>Contracts"]
    Controls["<b>Controls</b><br/>UI Components"]
    Resources["<b>Resources</b><br/>Static Assets"]
    Framework["<b>Framework</b><br/>Core Foundation"]
    
    App -->|References| Modules
    Modules -->|References| Shared
    Shared -->|References| Controls
    Controls -->|References| Resources
    Resources -->|References| Framework
    
    style App fill:#0d1f2d,stroke:#00a8e8,stroke-width:3px,color:#e0f0ff,font-family:Monaco,font-size:13px,font-weight:bold
    style Modules fill:#1e3a5f,stroke:#0066cc,stroke-width:2px,color:#e0f0ff,font-family:Monaco,font-size:13px,font-weight:bold
    style Shared fill:#2a5a7f,stroke:#0066cc,stroke-width:2px,color:#e0f0ff,font-family:Monaco,font-size:12px,font-weight:bold
    style Controls fill:#2a5a7f,stroke:#0066cc,stroke-width:2px,color:#e0f0ff,font-family:Monaco,font-size:12px,font-weight:bold
    style Resources fill:#3a7a9f,stroke:#0066cc,stroke-width:2px,color:#e0f0ff,font-family:Monaco,font-size:12px,font-weight:bold
    style Framework fill:#4a8a9f,stroke:#0066cc,stroke-width:2px,color:#e0f0ff,font-family:Monaco,font-size:12px,font-weight:bold

```

- **App**: Entry point project, handles startup and wires all modules together
- **Modules**: Individual feature modules (Social, Event Center, etc.), work independently
- **Shared**: Common interfaces and data models used across modules
- **Controls**: Custom control library, all hand-written controls live here
- **Resources**: Static assets like images and icons
- **Framework**: Base layer with MVVM base classes, attached properties, and common utilities

## <span style="color: #0969da;">Tech Stack</span>

Built with **Prism** for modular architecture, **DryIoc** for dependency injection, and standard MVVM pattern.

UI is completely hand-written, no ready-made UI libraries. Animations and transitions are all tweaked manually to match the game's feel.

For data handling, mainly **Mapster** for object mapping (faster than AutoMapper), and **Newtonsoft.Json** for JSON (old but gold).

## <span style="color: #0969da;">How to Run</span>

You'll need **Windows 10+** and **.NET 6 SDK** (or later).

Open the solution in Visual Studio 2022 and just run the `NarakaBladepoint.App` project.

### <span style="color: #0969da;">Dependencies</span>

Just three lightweight packages:
- Mapster 7.4.0
- Newtonsoft.Json 13.0.4
- Prism.DryIoc 9.0.537
