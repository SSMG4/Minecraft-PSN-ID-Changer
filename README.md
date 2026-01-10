# Minecraft-PSN-ID-Changer
Create custom usernames on Minecraft PS Vita via Vitacheat codes

[![Windows Release CI](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/build-release.yml/badge.svg?branch=master)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/windows-ci.yml)
[![Windows Debug CI](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/build-debug.yml/badge.svg?branch=master)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/windows-ci.yml)
[![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/SSMG4/Minecraft-PSN-ID-Changer/total)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/releases)
[![GitHub Release](https://img.shields.io/github/v/release/SSMG4/Minecraft-PSN-ID-Changer)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/releases/latest)
[![GitHub Repo stars](https://img.shields.io/github/stars/SSMG4/Minecraft-PSN-ID-Changer)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/stargazers)
[![GitHub Pull Requests](https://img.shields.io/github/issues-pr/SSMG4/Minecraft-PSN-ID-Changer)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/pulls)
[![GitHub Issues](https://img.shields.io/github/issues/SSMG4/Minecraft-PSN-ID-Changer)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/issues)

## Features
This tool allows you to personalize your Minecraft PS Vita experience by generating custom memory-injection codes.

* **Dynamic ID Generation**: Enter any username to generate a corresponding 32-bit VitaCheat code.
* **UTF-8 Support**: Supports special symbols (like § for colors) and foreign characters.
* **Region Compatibility**: Pre-configured support for European (PCSB00560), American (PCSE00491), and Japanese (PCSG00302) game versions.
* **Custom Code Naming**: Allows you to name your cheats within the generated `.psv` file for better organization.

## Downloads
You can obtain the application in three ways:

1. **Stable Releases**: Visit the [Releases](https://www.google.com/search?q=https://github.com/SSMG4/Minecraft-PSN-ID-Changer/releases) tab for the latest verified version.
2. **Nightly Builds**: Get the absolute latest automated builds via **nightly.link**:
* [Download Release Build](https://nightly.link/SSMG4/Minecraft-PSN-ID-Changer/workflows/build-release/master/Minecraft-PSN-ID-Changer-EXE.zip) (Recommended)
* [Download Debug Build](https://nightly.link/SSMG4/Minecraft-PSN-ID-Changer/workflows/build-debug/master/Minecraft-PSN-ID-Changer-DEBUG.zip) (For reporting issues)


3. **Source**: Build the application yourself from the source code provided in this repository.

## Building

If you wish to compile the source code yourself, follow these instructions.

### Requirements

* **Windows 10/11**
* **.NET Framework 4.7.2** (or higher)
* **Visual Studio** or **Build Tools for Visual Studio** (includes MSBuild)

### Option 1: Visual Studio Developer Command Prompt

This is the fastest way to build without opening the full Visual Studio interface.

1. Open the **Developer Command Prompt for VS** from your Start Menu.
2. Navigate to your project folder:
```cmd
cd C:\path\to\Minecraft-PSN-ID-Changer

```


3. Restore the project dependencies:
```cmd
nuget restore MinecraftIDChanger.sln

```


4. Compile the Release build:
```cmd
msbuild MinecraftIDChanger.sln /p:Configuration=Release /p:Platform="Any CPU"

```



### Option 2: Visual Studio / VS Code

1. **Open the Solution**: Launch `MinecraftIDChanger.sln` in your editor.
2. **Restore Packages**: Visual Studio will usually do this automatically on open.
3. **Compile**: Set the configuration dropdown to `Release` and select **Build > Build Solution**.

### Output Location

Once the build is finished, you can find your executable here:
`MinecraftIDChanger/bin/Release/MinecraftIDChanger.exe`

## More

* **License**: This project is licensed under the **GPL-v3.0 License**.
* **Author**: Developed by **SSMG4**.
* **Accuracy Disclaimer**: This is a recreated source code made from scratch. While functional, it may not be 100% accurate to the original Japanese tool it was inspired by.

&copy; 2026 SSMG4 All Rights Reserved.
