# 📦 Changelog – SimpleMenu

---

## 🆕 Version 1.2 – [2025-07-XX]

### 🚀 New Features

- **🔸 Custom Weapons Submenu**
  - Vehicle Gun (experimental)
  - Ped Gun (experimental)
  - Teleport Gun (experimental)

- **🔸 Wanted Settings Improvements**
  - Clear Wanted Level
  - Toggle Police Ignore Player
  - Toggle Dispatch Enabled
  - Disable Civilian Reports
  - Improved Never Wanted behavior (prevents alert blip spam & music flicker)

- **🔸 Teleport Enhancements**
  - **Important Teleports Submenu**
    - Teleport to nearest:
      - Gun Shops (Ammu-Nation)
      - Clothing Stores
      - Hospitals
      - Police Stations
    - Powered by `teleports.json` for external configurability
  - Improved Teleport to Waypoint — now includes vehicle-safe teleporting

- **🔸 Weapon Settings Additions**
  - Super Accuracy (tight spread)
  - No Recoil (removes vertical shake when firing)

- **🔸 Player Settings Additions**
  - Max All Player Stats (Stamina, Strength, Driving, etc.) [Story Mode only]
  - Change Player Model overhaul:
    - Load from `peds.json`
    - Random Outfit Generator
    - Prop/Component selectors
    - Reset to Michael, Franklin, or Trevor

- **🔸 Vehicle Settings Additions**
  - Vehicle Functions Submenu:
    - Open All Vehicle Doors (toggle)
    - Lock/Unlock Doors
    - Engine On/Off
    - Always Seatbelt (prevents ejection)

- **🔸 Menu Visuals / Quality of Life**
  - Optional menu themes system (WIP)
  - Better INI fallback behavior
  - Submenu memory improvements

---

## 🐞 Fixes & Stability

- Teleporting inside vehicles now functions reliably
- "Never Wanted" fix: prevents dispatch music from playing repeatedly
- JSON files are now validated for missing keys and malformed entries
- Safe fallback added for missing hotkeys or categories in INI

---

## 🧪 Experimental (New)

- Custom Weapons (Tele Gun, Ped Gun, Vehicle Gun): toggles work, but no functional shooting yet
- Cinematic Bars / Camera Shake: still visual placeholders (not supported in LemonUI)
- Player Max Stats: not always persistent in all story saves (GTA limitation)

---

## 🔁 Previous Versions

### 🧱 Version 1.1 – [2025-07-XX]
- Landmarks Teleports, HUD options, Bodyguards, Vision FX, .INI hotkeys

### 🧱 Version 1.0 – [2025-07-15]
- Initial release: basic menu, vehicle spawner, teleport, world/weather/time controls

---

