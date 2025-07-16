# 🎮 SimpleMenu for GTA V [Single Player] – v1.1

**SimpleMenu** is a lightweight, modular, and open-source GTA V mod menu designed for single player. Built using SHVDN3 and LemonUI, it offers essential gameplay enhancements and utilities — with a clean structure and expandability in mind.

---

## ✅ What's New in v1.1

- ✨ **New Submenus**:
  - **Landmarks Teleport** – 29+ real-world locations (airports, landmarks, military base, carrier, docks…)
  - **Abilities** – Super jump, fast run, disable ragdoll, drunk mode, etc.
  - **Player Vision** – Toggle Night Vision, Thermal Vision, Motion Blur, Camera Shake
  - **HUD Options** – Hide HUD, Radar, Wanted Stars, Cash, Time, etc.
  - **Misc Options** – Explode all cars, show speedometer, show coordinates, kill all enemies, force first-person view
  - **Bodyguards** – Spawn armed groups from factions (cops, SWAT, military, gangs) with godmode toggle
  - **About** – Displays mod info, credits, GitHub, etc.

- 🧠 **Menu State Memory**:
  - Menu now **remembers the last opened submenu** via the INI file.

- ⚙️ **Hotkey Support (via INI)**:
  Customize keybinds in `SimpleMenu.ini`:
[Hotkeys]
ToggleGodMode = NumPad1
GiveAllWeapons = NumPad2
FreezeAmmo = NumPad3
NeverWanted = NumPad4
ToggleSpeedometer = NumPad5
TeleportToWaypoint = NumPad6
ToggleFastRun = NumPad7
ToggleSuperJump = NumPad8


- 🧩 Modular Code Structure for Developers
- 📝 Updated readme, changelog, and code documentation

---

## 🧪 Known Bugs / Notes

See [`NOTES.md`](NOTES.md) for current known bugs, limitations, and experimental features.

---

## 📦 Installation

1. Requires:
 - ScriptHookV
 - ScriptHookVDotNet v3
 - LemonUI for SHVDN3 (v2.2+)
2. Drop files in your GTA V root folder:
/scripts/SimpleMenu.dll
/scripts/SimpleMenu.ini
/scripts/SM Data

---

## 🧰 Features Overview

- **Player Options**: God mode, heal, invisibility, abilities, models
- **Vehicle Options**: God mode, tuning, repair, turbo boost, spawn vehicles
- **Weapons**: Give all weapons, explosive melee, infinite ammo
- **Teleport**: Waypoint teleport, famous landmarks
- **World Control**: Weather, time, freeze time
- **NPC Settings**: Density control, chaos mode, clear area
- **Misc**: Kill enemies, HUD tweaks, explode vehicles, more

---

## 🛠️ Developers & Contributors

- 💻 **Main Developer**: [@Mo3izo](https://github.com/Mouataz86)
- 🎨 UI via [LemonUI (SHVDN3)](https://github.com/LemonUIbyLemon/LemonUI)
- 📚 JSON Support via Newtonsoft.Json

---

## 📂 Project Structure

/scripts/
├── SimpleMenu.dll
├── SimpleMenu.ini
└── SM Data/
└── vehicles.json


---

## 🪪 License

**MIT License** – Free to modify, share, and contribute. Please credit the original author.

---

## 🌐 Links

- 🔗 [GitHub Source](https://github.com/Mouataz86/SimpleMenu)
- 📄 [Changelog](CHANGELOG.md)
- 🧪 [Bugs & Notes](NOTES.md)

---

Enjoy the mod! Feedback, suggestions, and pull requests are welcome 🎉
