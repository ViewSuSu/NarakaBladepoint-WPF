[English](README.md) | [中文](README-zh.md)

# 永劫无间客户端 (WPF)

![.NET 6](https://img.shields.io/badge/.NET%206-Windows%20Desktop-purple)
![Platform](https://img.shields.io/badge/Platform-WPF-orange)
![License](https://img.shields.io/badge/license-MIT-lightgrey)

## 项目介绍

这是一个使用WPF技术重现永劫无间(Naraka Bladepoint)游戏客户端的项目。该项目基于.NET 6平台，采用现代化的WPF开发技术，展示了如何构建具有复杂UI交互的桌面应用程序。

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

## Main Interface Preview

<div align="center">
  <img src="./docs/MainWindows.png" alt="Main Window Interface" width="800" />
  <br/>
  <em>Figure 1: Main Window</em>
</div>

<table align="center">
  <tr>
    <td align="center">
      <img src="./docs/HeroList.png" alt="Hero List Interface" width="380" /><br/>
      <em>Figure 2: Hero List</em>
    </td>
    <td align="center">
      <img src="./docs/IllustratedCollection.png" alt="Illustrated Collection Interface" width="380" /><br/>
      <em>Figure 3: Illustrated Collection</em>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="./docs/PersonalInfo.png" alt="Personal Info Interface" width="380" /><br/>
      <em>Figure 4: Personal Info</em>
    </td>
    <td align="center">
      <img src="./docs/AvatarList.png" alt="Avatar List Interface" width="380" /><br/>
      <em>Figure 5: Avatar List</em>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="./docs/HistoryData.png" alt="History Data Interface" width="380" /><br/>
      <em>Figure 6: History Data</em>
    </td>
    <td align="center">
      <img src="./docs/SocialTag.png" alt="Social Tag Interface" width="380" /><br/>
      <em>Figure 7: Social Tag</em>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="./docs/Tag.png" alt="Tag Interface" width="380" /><br/>
      <em>Figure 8: Tag</em>
    </td>
    <td align="center">
      <img src="./docs/Weapon.png" alt="Weapon Interface" width="380" /><br/>
      <em>Figure 9: Weapon</em>
    </td>
  </tr>
</table>

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

- **Mapster** Version 7.4.0 - 高性能对象映射
- **Newtonsoft.Json** Version 13.0.4 - JSON数据处理
- **Prism.DryIoc** Version 9.0.537 - 模块化框架和依赖注入

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
