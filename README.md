![.NET 6](https://img.shields.io/badge/.NET%206-Windows%20Desktop-purple)
![Platform](https://img.shields.io/badge/Platform-WPF-orange)
![License](https://img.shields.io/badge/license-MIT-lightgrey)

[English](README-en.md) | [简体中文](README.md)

[![下载安装包](https://img.shields.io/badge/下载安装包-NarakaBladepoint--WPF.exe-blue?style=for-the-badge&logo=windows)](https://github.com/ViewSuSu/NarakaBladepoint-WPF/releases/download/v1.0.0/NarakaBladepoint-WPF.exe)

> [!TIP]
> 运行失败请确保已安装 [.NET 6.0 SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet/6.0) 或更高版本。



# 永劫无间客户端 (WPF)


## 项目介绍

这是一个使用WPF技术重现永劫无间(Naraka Bladepoint)游戏客户端的项目。该项目基于.NET 6平台，采用现代化的WPF开发技术，展示了如何构建具有复杂UI交互的桌面应用程序。

## 学习价值

本项目为WPF开发者提供以下学习机会：

1. **复杂UI开发**: 学习如何重现游戏级别的复杂界面
2. **Prism框架应用**: 掌握大型项目的模块化开发
3. **自定义控件**: 了解复杂自定义控件的设计与实现
4. **数据绑定**: 学习高效的数据绑定和状态管理
5. **性能优化**: 掌握WPF应用程序的性能优化技巧
6. **零依赖UI库**: 学习在不依赖第三方UI库的情况下，如何从零开始复现和定制复杂的自定义控件
7. **大型项目架构设计**: 学习大型WPF项目的合理架构设计，包括：
   - 依赖注入(IoC)的实现和应用
   - 基类的封装和继承设计
   - 模块化拆分和解耦策略
   - 可扩展性和可维护性的架构考虑

## 主要界面

<div align="center">
  <img src="./docs/MainWindows.png" alt="Main Window Interface" width="100%" />
  <br/>
  <em>Figure 1: 主界面</em>
</div>

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
      <img src="./docs/AvatarList.png" alt="Avatar List Interface" width="100%" /><br/>
      <em>Figure 5: 头像</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/HistoryData.png" alt="History Data Interface" width="100%" /><br/>
      <em>Figure 6: 历史记录</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/SocialTag.png" alt="Social Tag Interface" width="100%" /><br/>
      <em>Figure 7: 社交标签</em>
    </td>
  </tr>
  <tr>
    <td align="center" width="33.33%">
      <img src="./docs/Tag.png" alt="Tag Interface" width="100%" /><br/>
      <em>Figure 8: 标签</em>
    </td>
    <td align="center" width="33.33%">
      <img src="./docs/Weapon.png" alt="Weapon Interface" width="100%" /><br/>
      <em>Figure 9: 武器列表</em>
    </td>
    <td width="33.33%"></td>
  </tr>
</table>


## 项目架构

本项目采用基于 **Prism** 的模块化分层设计。

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

## 核心技术特点

### 1. 架构设计
- 采用Prism框架实现模块化开发
- 使用DryIoc作为依赖注入容器
- 基于MVVM模式的现代化架构

### 2. UI/UX实现
- 高质量还原永劫无间游戏界面
- 复杂的自定义控件开发
- 流畅的动画和过渡效果

### 3. 数据处理
- 使用Mapster进行高效对象映射
- Newtonsoft.Json处理JSON数据
- 数据绑定和状态管理

## 开发环境要求

### 系统要求
- Windows 10或更高版本
- .NET 6 SDK
- Visual Studio 2022或更高版本

## Build and Run

运行 `NarakaBladepoint.App`

- 环境: `net6-windows`
- IDE: Visual Studio 2022 或更高版本

## Dependencies

项目使用以下NuGet包:

- **Mapster** Version 7.4.0 
- **Newtonsoft.Json** Version 13.0.4 
- **Prism.DryIoc** Version 9.0.537 
