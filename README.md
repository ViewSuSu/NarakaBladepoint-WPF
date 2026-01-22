[English](README.md) | [中文](README-zh.md)

# Naraka Bladepoint Client (WPF)

![.NET 6](https://img.shields.io/badge/.NET%206-Windows%20Desktop-purple)
![Platform](https://img.shields.io/badge/Platform-WPF-orange)
![License](https://img.shields.io/badge/license-MIT-lightgrey)

## Project Introduction

This is a WPF project that recreates the Naraka Bladepoint game client interface. Based on the .NET 6 platform, this project utilizes modern WPF development techniques to demonstrate how to build desktop applications with complex UI interactions.

## Core Technical Features

### 1. Architecture Design
- Modular development using the Prism framework
- DryIoc as dependency injection container
- Modern architecture based on MVVM pattern

### 2. UI/UX Implementation
- High-quality recreation of Naraka Bladepoint game interface
- Development of complex custom controls
- Smooth animations and transition effects

### 3. Data Processing
- Efficient object mapping with Mapster
- JSON data processing with Newtonsoft.Json
- Data binding and state management

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

## Development Environment Requirements

### System Requirements
- Windows 10 or later
- .NET 6 SDK
- Visual Studio 2022 or later

## Build and Run

Run `NarakaBladepoint.App`

- Environment: `net6-windows`
- IDE: Visual Studio 2022 or later

## Dependencies

The project uses the following NuGet packages:

- **Mapster** Version 7.4.0 - High-performance object mapping
- **Newtonsoft.Json** Version 13.0.4 - JSON data processing
- **Prism.DryIoc** Version 9.0.537 - Modular framework and dependency injection

## Learning Value

This project provides the following learning opportunities for WPF developers:

1. **Complex UI Development**: Learn how to recreate game-level complex interfaces
2. **Prism Framework Application**: Master modular development for large projects
3. **Custom Controls**: Understand the design and implementation of complex custom controls
4. **Data Binding**: Learn efficient data binding and state management
5. **Performance Optimization**: Master WPF application performance optimization techniques
6. **Zero Dependency UI Library**: Learn how to recreate and customize complex custom controls from scratch without relying on third-party UI libraries
7. **Large Project Architecture Design**: Learn reasonable architecture design for large WPF projects, including:
   - Implementation and application of dependency injection (IoC)
   - Base class encapsulation and inheritance design
   - Modularization, splitting, and decoupling strategies
   - Architecture considerations for scalability and maintainability

## Contribution Guidelines

Contributions to the Naraka Bladepoint Client (WPF) project are welcome! Please follow these steps:

1. Fork this repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For questions or suggestions, please submit feedback via GitHub Issues.

---
