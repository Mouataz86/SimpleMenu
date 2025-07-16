<h1 align="center">Simple Mod Menu For GTA V Single Player [ Open Source ]</h1>
<h2 align="center">The Latest Version Currently Is: V1.1</h2>

<p align="center">
  <img src="https://img.shields.io/badge/version-v1.1-blue" alt="Version">
  <img src="https://img.shields.io/badge/platform-PC-brightgreen" alt="Platform">
  <img src="https://img.shields.io/badge/license-MIT-lightgrey" alt="License">
</p>

<p align="center">
  <i>Customize, control, and conquer Los Santos with ease.</i>
</p>

**SimpleMenu** is a powerful yet lightweight open-source C# mod menu for **Grand Theft Auto V** single player. It offers player abilities, vehicle management, weather control, teleportation, vision effects, and much more through an in-game LemonUI-powered interface.

---

## 📑 Table of Contents

- [Features](#-features)
- [Requirements](#-requirements)
- [Installation](#-installation)
- [Usage](#-usage)
- [File Structure](#-file-structure)
- [Developer Info](#-developer-info)
- [Contributing](#-contributing)
- [Credits](#-credits)
- [License](#-license)

---

## ✅ Features

- 🚶‍♂️ **Player Settings**: God mode, heal, ragdoll toggle, fast run/swim, super jump, explosive melee, super punch, invisibility, drunk mode.
- 🚗 **Vehicle Settings**: Repair/destroy, upgrade, mod tuning, neon lighting, utility toggles (auto-flip, turbo, stick to ground).
- 🚙 **Spawn Menu**: Choose from hundreds of categorized vehicles (Sports, Planes, Boats, Emergency, etc.) using external `vehicles.json`.
- 🧭 **Teleportation**: Instantly move to waypoint or mission objective.
- 🔫 **Weapons**: Give all weapons, max ammo, infinite ammo, explosive melee.
- 🚓 **Wanted Level Control**: Set/clear/freeze wanted level, never wanted mode.
- 👁️ **Player Vision FX**: Night vision, thermal vision, motion blur, cinematic bars, camera shake.
- 🎛️ **Customizable INI Config**: Modify `OpenKey` and menu preferences.
- 🧩 **Modular Code**: Easy to extend with additional LemonUI menus.
- 🛠️ **LemonUI Powered**: Clean and fast native-style UI framework.

---

## 🔧 Requirements

Before installing, ensure you have the following:

- [ScriptHookV](http://www.dev-c.com/gtav/scripthookv/)
- [ScriptHookVDotNet 3](https://github.com/crosire/scripthookvdotnet)
- [.NET Framework 4.8+](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
- [LemonUI (SHVDN3)](https://github.com/LemonUIbyLemon/LemonUI)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)

---

## 📦 Installation

1. Download the latest release from the [releases](https://github.com/Mouataz86/SimpleMenu/releases) page.
2. Extract the files.
3. Copy the following to your `Grand Theft Auto V/scripts/` folder:
- SimpleMenu.dll
- SimpleMenu.ini
- Newtonsoft.Json.dll
- LemonUI.SHVDN3.dll
- SM Data ( Contains Vehicles.json )


4. Launch the game and press your configured menu key (default is `F10`).

---

## 🎮 Usage

- Press your configured key (`F10` by default) to toggle the menu.
- Use arrow keys or controller to navigate.
- Toggle features, spawn vehicles, modify player and world state.

---

## 📁 File Structure

Grand Theft Auto V/
- SimpleMenu.dll
- SimpleMenu.ini
- Newtonsoft.Json.dll
- LemonUI.SHVDN3.dll
- SM Data ( Contains Vehicles.json )


- `SimpleMenu.dll` — The compiled mod menu script.
- `SimpleMenu.ini` — Holds user-editable menu preferences.
- `vehicles.json` — All categorized spawnable vehicles.

---

## 👨‍💻 Developer Info

- 💡 The project uses **LemonUI** for all menu rendering.
- 📦 Vehicle categories are loaded from `vehicles.json` using **Newtonsoft.Json**.
- 🎯 You can expand the mod by:
  - Adding more submenus with `NativeMenu`.
  - Appending new vehicle categories to `vehicles.json`.
  - Editing the `SimpleMenu.ini` to customize behavior.

---

## 🤝 Contributing

Want to improve this mod?

1. Fork this repository
2. Create a feature branch (`git checkout -b feature/some-feature`)
3. Commit your changes
4. Push to your fork and submit a pull request

For developers: please see `README-DEV.md` for structure details.

---

## ✨ Credits

- **@Mouataz86** – Lead developer & project maintainer.
- **LemonUI by Justalemon** – [LemonUI GitHub](https://github.com/LemonUIbyLemon/LemonUI)
- **Newtonsoft.Json by James Newton-King** – [Official Site](https://www.newtonsoft.com/json)
- **ScriptHookVDotNet & ScriptHookV authors** – Scripting foundation.

---

## 📜 License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.

