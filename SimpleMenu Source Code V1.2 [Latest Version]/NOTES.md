# ⚠️ Known Bugs & Experimental Features (v1.2)

---

## ❗ Known Issues (v1.2)

### 1. 🎥 Cinematic Bars & Camera Shake
- LemonUI SHVDN3 currently **does not support real cinematic bars or camera shake**.
- The options exist in the menu but **have no visual effect**.
- They are retained as placeholders for potential future support.

### 2. 🔫 Custom Weapons (Vehicle / Ped / Teleport Guns)
- These weapons are **currently non-functional**.
- The menu displays activation notifications only — no projectile replacement or teleportation is applied yet.
- Planned for future SHVDN-native logic integration or ScriptHookV patches.

### 3. 🚗 Open All Doors Toggle
- The toggle may fail to **re-close** some doors depending on:
  - Vehicle class (e.g., emergency vehicles, aircraft)
  - Number of valid doors (`DOES_VEHICLE_HAVE_DOOR` false)
- May need manual override logic per model in future.

### 4. 🎯 Max Stats Not Applying
- "Max All Stats" feature may **appear ineffective** in some story saves.
- STAT_SET_INT may not immediately update UI or may require:
  - Game reload
  - Mission trigger
  - Stat recalculation by engine
- Currently **tested only on Franklin**, effect is delayed or inconsistent on Michael/Trevor.

### 5. 🚨 Police & Wanted Conflicts
- When using "Never Wanted" + entering restricted zones (e.g., airport):
  - **Chase music may briefly trigger**
  - Radar flashes (red/blue blip blink)
  - Game AI may react before the script clears wanted level
- Currently unavoidable due to engine delays in wanted system reset.

---

## 🧪 Experimental or Under Testing (v1.2)

### 1. 🔫 Custom Weapons Menu
- Experimental submenu added for:
  - Vehicle Gun
  - Ped Gun
  - Teleport Gun
- No gameplay logic hooked yet — just placeholder toggles and notices.

### 2. 🧍‍♂️ Model/Outfit Component Editing
- Prop/component slots (e.g. hats, torso) **may be incompatible** with some ped models.
- Certain NPCs crash if invalid drawable/texture index applied.
- Works best with `mp_m_freemode_01` and `mp_f_freemode_01`.

### 3. 🧭 Important Teleports (teleports.json)
- Teleport locations load correctly, but:
  - Some coordinates still place player **just inside** doors or undesired spawn points.
  - Currently refined only for ~8 core locations (gunshops, hospitals, PD).
- Further user testing needed to polish spawn markers.

---

## 🔒 Stability & Safety Notes

- All newly added menus use null checks and graceful fails.
- `teleports.json` is fully sandboxed; missing or malformed entries will not crash menu.
- INI and JSON changes are hot-reloadable after restart.

---

## 📝 Suggestions & Feedback

Help us test, improve, or contribute:
📬 GitHub Issues → [https://github.com/Mouataz86/SimpleMenu/issues](https://github.com/Mouataz86/SimpleMenu/issues)

PRs and feedback welcome!

---
