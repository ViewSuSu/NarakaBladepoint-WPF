![.NET 6](https://img.shields.io/badge/.NET%206-Windows%20Desktop-purple)
![Platform](https://img.shields.io/badge/Platform-WPF-orange)
![License](https://img.shields.io/badge/license-MIT-lightgrey)

[English](README-en.md) | [简体中文](README.md)

[![下载最新安装包](https://img.shields.io/badge/下载最新安装包-NarakaBladepoint--WPF.exe-blue?style=for-the-badge&logo=windows)](https://github.com/ViewSuSu/NarakaBladepoint-WPF/releases/latest/download/NarakaBladepoint-WPF.exe)

> [!TIP]
> 运行失败请确保已安装 [.NET 6.0 SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet/6.0) 或更高版本。



# 永劫无间客户端 (WPF)


## 关于这个项目

- **初心** - 玩了2000个小时的永劫了，最近才发现它整个客户端UX/UI设计的非常牛逼（大厂的产品经理还是牛逼），故想试试用 WPF 还原出U3D的感觉
- **技术栈** - .NET 6、Prism、所有自定义控件纯手撸、没用第三方 UI 库
- **架构设计** - 经过精心设计，严格遵循 MVVM 模式，符合 WPF 工程规范，代码结构优雅
- **AI** - 后来发现 VibeCoding 太香了，用来堆量实现功能
- **代码质量** - 项目一半人味一半 AI 味，但架构和核心设计不依靠 AI

<div align="center">
  <img src="./docs/MainWindows.png" alt="主界面" width="100%" />
  <br/>
  <em>主界面效果</em>
</div>

## 你能从这个项目学到什么

如果你也在做 WPF 开发，这个项目可能对你有点帮助：

- **游戏级界面怎么做** - 复杂的布局、动画、交互效果，都是实际碰到并解决的问题
- **Prism 模块化实践** - 怎么把一个大项目拆成模块，各自独立又能协作
- **从零开始写控件** - 没用第三方库，所有自定义控件都是手撸的，能看到很多细节
- **数据绑定的技巧** - MVVM 模式下怎么优雅地处理数据流
- **性能优化** - 界面复杂了之后会遇到的性能问题和解决方案
- **项目架构设计** - 依赖注入、基类设计、模块解耦这些在实际项目里怎么用

有些地方写得不够好，欢迎提 issue 或 PR。

## 更多界面

<table align="center" width="100%">
  <tr>
    <td align="center" width="33.33%">
      <img src="./docs/HeroList.png" alt="Hero List Interface" width="100%" /><br/>
      <em>Figure 2: 英雄列表</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/IllustratedCollection.png" alt="Illustrated Collection Interface" width="100%" /><br/>
      <em>Figure 3: 仓库</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/PersonalInfo.png" alt="Personal Info Interface" width="100%" /><br/>
      <em>Figure 4: 个人信息</em>
    </td>
  </tr>
  <tr>
    <td align="center" width="33.33%">
      <img src="./docs/TippingRecord.png" alt="打赏记录" width="100%" /><br/>
      <em>Figure 5: 打赏记录</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/AvatarList.png" alt="Avatar List Interface" width="100%" /><br/>
      <em>Figure 6: 头像</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/HistoryData.png" alt="History Data Interface" width="100%" /><br/>
      <em>Figure 7: 历史记录</em>
    </td>
  </tr>
  <tr>
    <td align="center" width="33.33%">
      <img src="./docs/SocialTag.png" alt="Social Tag Interface" width="100%" /><br/>
      <em>Figure 8: 社交标签</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/Tag.png" alt="Tag Interface" width="100%" /><br/>
      <em>Figure 9: 标签</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/Weapon.png" alt="Weapon Interface" width="100%" /><br/>
      <em>Figure 10: 武器列表</em>
    </td>
  </tr>
</table>


## 项目架构

用的是 **Prism** 做模块化分层，整体结构大概是这样：

```mermaid
graph LR
    App["<b>App</b><br/>应用入口"]
    Modules["<b>Modules</b><br/>业务逻辑"]
    Shared["<b>Shared</b><br/>共享协议"]
    Controls["<b>Controls</b><br/>UI组件库"]
    Resources["<b>Resources</b><br/>静态资源"]
    Framework["<b>Framework</b><br/>基础框架"]
    
    App -->|引用| Modules
    Modules -->|引用| Shared
    Shared -->|引用| Controls
    Controls -->|引用| Resources
    Resources -->|引用| Framework
    
    style App fill:#0d1f2d,stroke:#00a8e8,stroke-width:3px,color:#e0f0ff,font-family:Monaco,font-size:13px,font-weight:bold
    style Modules fill:#1e3a5f,stroke:#0066cc,stroke-width:2px,color:#e0f0ff,font-family:Monaco,font-size:13px,font-weight:bold
    style Shared fill:#2a5a7f,stroke:#0066cc,stroke-width:2px,color:#e0f0ff,font-family:Monaco,font-size:12px,font-weight:bold
    style Controls fill:#2a5a7f,stroke:#0066cc,stroke-width:2px,color:#e0f0ff,font-family:Monaco,font-size:12px,font-weight:bold
    style Resources fill:#3a7a9f,stroke:#0066cc,stroke-width:2px,color:#e0f0ff,font-family:Monaco,font-size:12px,font-weight:bold
    style Framework fill:#4a8a9f,stroke:#0066cc,stroke-width:2px,color:#e0f0ff,font-family:Monaco,font-size:12px,font-weight:bold

```

- **App**: 入口 Shell，负责容器初始化与模块聚合。
- **Modules**: 功能业务层（社交、活动中心等），模块间通过 Region 隔离。
- **Shared**: 跨模块协议（DTO、接口契约、服务抽象）。
- **Controls**: 复用组件库，包含仿真的自定义控件库。
- **Resources**: 静态素材（图标、背景等）。
- **Framework**: 核心基础支撑层，提供 MVVM 基类、附加属性、弱事件等，由上述各层直接使用。

## 如何调试

 **Windows 10+** 和 **.NET 6 SDK**（或者更高版本）。

 Visual Studio 2022及以上打开解决方案，直接运行 `NarakaBladepoint.App` 项目就行。

## 第三方库
- Mapster 7.4.0
- Newtonsoft.Json 13.0.4
- Prism.DryIoc 9.0.537
