# Minecraft PS Vita PSN ID Changer

Create custom usernames on Minecraft PS Vita via VitaCheat codes — available as a **Windows desktop app** (C# / .NET 8) and an **Android app** (Kotlin).

[![Windows Release CI](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/build-release.yml/badge.svg?branch=master)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/build-release.yml)
[![Windows Debug CI](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/build-debug.yml/badge.svg?branch=master)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/build-debug.yml)
[![Android Release CI](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/android-release.yml/badge.svg?branch=master)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/android-release.yml)
[![Android Debug CI](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/android-debug.yml/badge.svg?branch=master)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/actions/workflows/android-debug.yml)
[![GitHub Downloads](https://img.shields.io/github/downloads/SSMG4/Minecraft-PSN-ID-Changer/total)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/releases)
[![GitHub Release](https://img.shields.io/github/v/release/SSMG4/Minecraft-PSN-ID-Changer)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/releases/latest)
[![GitHub Stars](https://img.shields.io/github/stars/SSMG4/Minecraft-PSN-ID-Changer)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/stargazers)
[![GitHub Pull Requests](https://img.shields.io/github/issues-pr/SSMG4/Minecraft-PSN-ID-Changer)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/pulls)
[![GitHub Issues](https://img.shields.io/github/issues/SSMG4/Minecraft-PSN-ID-Changer)](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/issues)

---

## Features

- **Dynamic ID Generation** — enter any username to generate the corresponding 32-bit VitaCheat patch code.
- **UTF-8 Support** — supports special symbols (e.g. `§` for colour codes) and non-ASCII characters.
- **Null-terminator fix** — always appends a zero block at the next aligned address, so switching from a longer name to a shorter one never leaves garbage characters.
- **Region Compatibility** — pre-configured support for EU (`PCSB00560`), US (`PCSE00491`), and JP (`PCSG00302`) game versions.
- **Custom Code Naming** — label your cheat entry inside the `.psv` file for better organisation.
- **Character counter** — real-time feedback showing how many characters you've used against the PSN 16-character limit.
- **Clear button** — reset all fields in one tap/click.
- **Android** — copy to clipboard, share via any app, or save directly to storage via the system file picker.

---

## Downloads

| Platform | Release | Debug (nightly) |
|----------|---------|-----------------|
| Windows `.exe` | [Latest Release](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/releases/latest) | [nightly.link](https://nightly.link/SSMG4/Minecraft-PSN-ID-Changer/workflows/build-debug/master/Windows-DEBUG.zip) |
| Android `.apk` | [Latest Release](https://github.com/SSMG4/Minecraft-PSN-ID-Changer/releases/latest) | [nightly.link](https://nightly.link/SSMG4/Minecraft-PSN-ID-Changer/workflows/android-debug/master/Android-DEBUG-APK.zip) |

---

## Repository structure

```
Minecraft-PSN-ID-Changer/
│
├── MinecraftIDChanger.sln               ← Visual Studio solution
├── MinecraftIDChanger/                  ← Windows WinForms project (.NET 8)
│   ├── MinecraftIDChanger.csproj
│   ├── Program.cs
│   ├── Form1.cs
│   └── Form1.Designer.cs
│
├── android/                             ← Android Kotlin project
│   ├── app/
│   │   ├── build.gradle.kts
│   │   └── src/main/
│   │       ├── java/com/ssmg4/minecraftidchanger/
│   │       │   ├── MainActivity.kt
│   │       │   └── VitaCheatGenerator.kt
│   │       ├── res/
│   │       └── AndroidManifest.xml
│   ├── build.gradle.kts
│   ├── settings.gradle.kts
│   └── gradle/libs.versions.toml
│
├── .github/workflows/
│   ├── build-debug.yml                  ← Windows Debug
│   ├── build-release.yml                ← Windows Release
│   ├── android-debug.yml                ← Android Debug + unit tests
│   ├── android-release.yml              ← Android Release + signing
│   └── ci-all-platforms.yml             ← All 4 jobs in parallel
│
├── .gitattributes
├── .gitignore
├── LICENSE
└── README.md
```

---

## Building

### Windows (requires Windows 10/11)

**Requirements:** [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

```cmd
REM Restore & build (from repo root)
dotnet restore MinecraftIDChanger.sln
dotnet build   MinecraftIDChanger.sln --configuration Release

REM Or publish as a single self-contained .exe
dotnet publish MinecraftIDChanger/MinecraftIDChanger.csproj ^
  --configuration Release ^
  --runtime win-x64 ^
  --self-contained false ^
  --output publish/ ^
  -p:PublishSingleFile=true
```

Output: `MinecraftIDChanger/bin/Release/net8.0-windows/MinecraftIDChanger.exe`

---

### Android

**Requirements:** [Android Studio Ladybug+](https://developer.android.com/studio) or the Android command-line tools, JDK 17.

```bash
cd android

# Run unit tests
./gradlew :app:testDebugUnitTest

# Build debug APK
./gradlew :app:assembleDebug

# Build release APK (unsigned)
./gradlew :app:assembleRelease
```

Output: `android/app/build/outputs/apk/`

#### Signing a release APK

1. Generate a keystore: `keytool -genkey -v -keystore release.jks -alias mykey -keyalg RSA -keysize 2048 -validity 10000`
2. Add signing config to `android/app/build.gradle.kts` inside `buildTypes { release { signingConfig = ... } }`.
3. For CI signing, store the base64 keystore as the `KEYSTORE_BASE64` repository secret (see `android-release.yml`).

---

## How it works

The PS Vita stores the player's username as a UTF-8 string in memory starting at address `0x8234628D`. VitaCheat patches this memory at boot using `$0200` (32-bit write) opcodes. This tool:

1. Encodes your chosen username to UTF-8.
2. Splits the bytes into 4-byte little-endian chunks.
3. Emits one `$0200 <address> <value>` line per chunk.
4. Appends a null-terminator block at the next aligned address to safely overwrite any previously longer name.

The generated `.psv` file is placed in the VitaCheat cheats folder for your game region on the PS Vita's memory card.

---

## More

- **License**: This project is licensed under the **GPL-v3.0 License**.
- **Author**: Developed by **SSMG4**.
- **Accuracy Disclaimer**: This is a recreated source code made from scratch. While functional, it may not be 100% accurate to the original Japanese tool it was inspired by.

&copy; 2026 SSMG4 All Rights Reserved.
