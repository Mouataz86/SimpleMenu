<h1 align="center">SimpleMenu for GTA V Single Player – Open Source Mod Menu</h1>
<h2 align="center">Current Stable Version: v1.2</h2>

<p align="center">
  <img src="https://img.shields.io/badge/version-v1.2-blue" alt="Version">
  <img src="https://img.shields.io/badge/platform-PC-brightgreen" alt="Platform">
  <img src="https://img.shields.io/badge/license-MIT-lightgrey" alt="License">
</p>

<p align="center">
  <i>A clean, customizable, and powerful native-style mod menu for single-player Grand Theft Auto V.</i>
</p>

---

## Overview

**SimpleMenu** is a fast, modular, and open-source C# mod menu for GTA V (Story Mode only). Built with LemonUI for SHVDN3, it provides a native-style menu packed with tools to manipulate the player, vehicles, weather, time, weapons, NPCs, teleports, and more — all accessible in-game with no trainer overlays required.

---

## Features

### Player Settings
- Toggle God Mode, Heal, Invisibility, Max Health/Armor
- Super Jump, Fast Run, Fast Swim
- Explosive Melee, Flaming Bullets, Super Punch
- Disable Ragdoll, Drunk Mode
- Player Model Changer (supports custom JSON peds)
- Outfit Customization (components, props, reset options)
- Random Outfit Generator
- Max Out Player Stats (Stamina, Strength, Shooting, etc.)

### Vehicle Settings
- Full Vehicle Tuning (mods, neon, colors, wheels)
- Turbo Boost, Auto Repair, Stick to Ground, Engine Control
- Vehicle Spawner (from editable `vehicles.json`)
- Lock/Unlock All Doors, Open All Doors
- Always Seatbelt Mode (prevents crash ejection)

### Weapon Options
- Give All Weapons, Remove All, Infinite Ammo
- Freeze Ammo Count, Explosive Bullets, Flaming Ammo
- Super Accuracy, No Recoil
- Experimental Custom Weapons: 
  - Vehicle Gun (shoots cars)
  - Ped Gun (spawns peds at bullet impact)
  - Teleport Gun (teleports player on shot)

### World & Weather
- Time Control: Set Hour, Freeze Time
- Weather: Change or freeze to any weather state
- Vision FX: Night Vision, Thermal Vision, Motion Blur, Cinematic Bars, Camera Shake

### NPC & Wanted Level
- Toggle Dispatch
- Max/Freeze/Clear Wanted Level
- Never Wanted
- Disable Civilian Reports
- Everyone Ignores Player
- Chaos Mode (aggressive NPCs)
- Control Vehicle & Ped Density

### Teleportation
- Teleport to Waypoint
- Famous Landmarks Menu (Maze Tower, Carrier, etc.)
- Important Teleports (Ammu-Nations, Police Stations, Clothing Stores, Hospitals)
- Supports loading teleports via `teleports.json`

### Bodyguards
- Spawn Companions: SWAT, Military, Gangs (Families, Ballas, Vagos)
- Custom Vehicles, Godmode toggle
- Fully armed and follow player

### Misc Options
- Toggle HUD Elements: Radar, Wanted Stars, Time, Crosshair, Area Name, Cash
- Speedometer On/Off
- Show Player Coordinates
- Explode All Vehicles
- Kill All Enemies
- Force First-Person View

---

## New in Version 1.2
- Added **Wanted Settings** submenu
- Added **Custom Weapons** submenu
- Added **Vehicle Functions** submenu
- Added **Player Stats Maxer** (Story characters only)
- Expanded Teleports via JSON: Ammu-Nations, Hospitals, Clothing, Police
- Reworked **Model & Outfit Editor**
- Improved INI logic for submenu memory
- Added **Super Accuracy** and **No Recoil** options

---

## Known Bugs & Limitations
- **Cinematic Bars & Camera Shake**: Visual effects are placeholders (LemonUI SHVDN3 does not support them yet)
- **Top-down view** was removed (conflicted with camera system)
- **Custom Weapons** are experimental (Teleport/Ped/Vehicle Guns)
- **Kill All Enemies** may not affect hostile animals or spawned bodyguards
- **Player Stat Maxer** works only for Michael, Franklin, Trevor (Story Mode only)

---

## Requirements

- [ScriptHookV](http://www.dev-c.com/gtav/scripthookv/)
- [ScriptHookVDotNet v3](https://github.com/crosire/scripthookvdotnet)
- [.NET Framework 4.8+](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
- [LemonUI for SHVDN3](https://github.com/LemonUIbyLemon/LemonUI)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)

---

## Installation

1. Download the latest release from the [Releases](https://github.com/Mouataz86/SimpleMenu/releases) page.
2. Install ScriptHookV and ScriptHookVDotNet3 as required.
3. Copy the contents of the archive into your GTA V `scripts` folder.
4. Launch the game and press `F10` (or the configured key) to open the menu.

---

## Hotkeys (SimpleMenu.ini)

```ini
[Menu]
OpenKey = F10
LastUsedTab = SimpleMenu

[Hotkeys]
ToggleGodMode = NumPad1
GiveAllWeapons = NumPad2
FreezeAmmo = NumPad3
NeverWanted = NumPad4
ToggleSpeedometer = NumPad5
TeleportToWaypoint = NumPad6
ToggleFastRun = NumPad7
ToggleSuperJump = NumPad8
```
## Developer Info

- **Telegram:** https://t.me/MO3IZO
- **E-mail:** admin@mo3izo-tech.tk
- **Paypal ( If You Want To Support ):contact@mo3izo-tech.tk**
- **Gta5-Mods.com:** https://www.gta5-mods.com/users/Mouataz

## Contributing
Want to help improve the mod?

Fork the repository

Submit pull requests

Suggest features via GitHub Issues

## Credits
Developed by @Mouataz86

LemonUI by Justalemon

Newtonsoft.Json by James Newton-King

ScriptHookVDotNet by Crosire

Special thanks to GitHub testers and community feedback

## License
This project is licensed under the MIT License — free to use, modify, and distribute.

