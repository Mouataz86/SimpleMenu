# ⚠️ Known Bugs & Experimental Features

---

## ❗ Known Issues

### 1. 🛑 Camera / View Modes
- **Top-down view toggle** has been removed. It forced 1st-person mode and caused unintended behavior.
- Replaced with simple **"Force First Person Mode"** toggle.

### 2. 🎥 Cinematic Bars & Camera Shake
- LemonUI SHVDN3 currently **does not support dynamic camera shake or cinematic black bars.**
- Options are available but do not visually reflect changes yet. Considered placeholders.

### 3. 🚗 Vehicle Color Index Mismatch
- Some vehicle color names (like "Black") may apply different colors due to internal GTA5 color indexes.
- Working on mapping proper visual names in a future update.

### 4. 💥 Kill All Enemies Logic
- May not affect:
  - Wild attacking animals (e.g., cougars)
  - All law enforcement
  - Peds spawned via bodyguard menu

---

## 🧪 Experimental or Under Testing

### 1. 🔧 INI Hotkeys
- Supported hotkeys (NumPad1–8) are functional for most options.
- More validation logic will be added to prevent user error.

### 2. 🔫 Super Punch
- Uses explosion-based knockback, which is **tuned down**.
- Works well, but limited due to GTA5 collision hitboxes.

### 3. 🎨 Menu Themes (Postponed)
- PNG-based banners were created, but LemonUI (SHVDN3) **does not support banner image overrides**.
- Considered for future if LemonUI adds `SetBanner()` support.

---

## 🔒 Stability Notes

- JSON and INI parsers are lightweight and error-tolerant.
- Menu will fallback to safe defaults if values are missing or invalid.

---

## 📝 Suggestions

Want to help improve the mod? Submit an issue or pull request on GitHub:
[https://github.com/Mouataz86/SimpleMenu](https://github.com/Mouataz86/SimpleMenu)

---
