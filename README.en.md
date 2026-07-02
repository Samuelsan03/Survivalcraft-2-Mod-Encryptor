# Survivalcraft 2 Mod Encryptor

## 📖 Description

**Survivalcraft 2 Mod Encryptor** is a mini-mod that restores the **Reinforcing** functionality. This feature allows you to encrypt `.scmod` files to add an extra layer of security to your mods. When used, your file becomes protected and can no longer be extracted as a conventional ZIP archive, helping to prevent theft or unauthorized extraction of your mod's resources.

This useful feature was available in Survivalcraft version **2.3**, but was removed without apparent reason in version **2.4** (with APIs 1.8 and 1.9). This mini-mod allows you to recover it, so you can protect your projects without having to rely on an old version of the game just for encryption.

**Important**: Unlike the old system that saved files in the `ModsCache` folder, this improved version creates an **independent** folder called `EncryptedMods`. This keeps your original mods and encrypted ones perfectly separated and organized.

### ✨ Key Features

*   **One-click encryption**: Protect any installed mod directly from the game's settings menu.
*   **Independent folder**: Encrypted files are saved in `EncryptedMods`, separate from original mods and cache.
*   **Full mobile compatibility**: Fixed the folder access error on Android, now works flawlessly on phones.
*   **Simple interface**: Intuitive dialogs guide you through the entire process.
*   **Duplicate protection**: Automatically names files to avoid overwriting others.
*   **Smart detection**: Recognizes if a mod is already encrypted to avoid unnecessary processes.
*   **Error handling**: Clear messages to help you with any issues.

---

## 📋 Usage Guide

### Step 1: Access the Settings Menu
<img width="1912" height="985" alt="image" src="https://github.com/user-attachments/assets/cdb2f24b-eb72-40cd-bc39-6918dcc0f7e9" />

On the main game screen, select **"CONFIGURACION"** to enter settings.

### Step 2: Find the Encrypt Button
<img width="1917" height="972" alt="image" src="https://github.com/user-attachments/assets/d28ea100-ef2a-47cd-a57d-8d2a8f9d1ebc" />

Within the settings menu, look for the **"ENCRIPTAR MOD"** option and click on it.

### Step 3: Select the Mod to Encrypt
<img width="1917" height="927" alt="image" src="https://github.com/user-attachments/assets/e076abb5-32a5-4d34-9846-9adaf5b1076b" />

A list of all your installed mods will appear. Select the one you want to protect.

### Step 4: Confirmation and Encryption
<img width="1917" height="967" alt="image" src="https://github.com/user-attachments/assets/2c76901b-2e6b-45e6-8352-63e814037ee9" />
<img width="1352" height="757" alt="image" src="https://github.com/user-attachments/assets/02abf2e8-b14c-49b8-a543-9e390d896da6" />

When finished, you'll see a confirmation message: **"EL MOD SE HA ENCRIPTADO CORRECTAMENTE."** (The mod has been successfully encrypted.)
The encrypted file will be saved to the `EncryptedMods` folder within the game's directory.

### Step 5: Locate the Encrypted File
<img width="792" height="757" alt="image" src="https://github.com/user-attachments/assets/42958f5c-5da8-42f8-83b4-66f11d0d20c0" />
<img width="788" height="761" alt="image" src="https://github.com/user-attachments/assets/c1130fc2-9055-43d9-9e47-301d7caafcb1" />

You can find your encrypted mod at:
```
📁 EncryptedMods
  └── [2.4]Cloud Backpack (Encrypted).scmod
```

**Storage system note**:
- **Version 2.3 (old)**: Saved files in `ModsCache`.
- **Version 2.4 with this mod (new)**: Saves files in `EncryptedMods` (independent folder).

This means your original mods stay intact in the `Mods` folder, encrypted files are saved separately, and they don't interfere with the game's cache.

### ⚠️ Important Note About the File
<img width="1107" height="757" alt="image" src="https://github.com/user-attachments/assets/14f915d2-fa8a-44bb-a796-abac168b0b60" />

**Don't panic if you see this message!**
When trying to open the encrypted `.scmod` file directly, you might see:
> "The file has an unknown format or is damaged"

**This is not an error**, but the correct functioning of the encryption. The file is protected and its structure has been deliberately obfuscated, so standard viewers won't be able to read it. However, it remains fully functional for compatible mod loaders.

---

## 💡 Advantages of Using This Encryptor

*   **Protect your work**: Prevent others from modifying or stealing your mod's resources without permission.
*   **Maintain integrity**: Ensure your mod works as you designed it.
*   **Easy to use**: No technical knowledge required to protect your files.
*   **Safe**: Original files are not modified, only an encrypted copy is created.
*   **Organization**: The `EncryptedMods` folder keeps your protected files separate and organized.
*   **Convenience**: Recover a lost feature without having to switch game versions.
*   **No conflicts**: By not using `ModsCache`, you avoid potential conflicts with other game systems.
*   **Fully functional on Android**: The permission error that prevented folder creation on mobile devices has been fixed.

---

## 🔧 Mobile Device Bug Fix

In previous versions, attempting to encrypt on Android would cause an access error:
```
ERROR: Access denied to "app:/EncryptedMods".
```
This happened because the `app:/EncryptedMods` path wasn't correctly recognized in the game's file system.

**This version fixes the issue** by using the correct path via `ModsManager.ExternalPath + "/EncryptedMods"`, which ensures the folder is created in the proper location with the necessary permissions. Now, mobile players can encrypt their mods without any issues.

---

## ❓ Troubleshooting

*   **"File not found"**: Make sure the selected mod exists in the `Mods` folder.
*   **"Invalid extension"**: Only `.scmod` files can be encrypted.
*   **"Already encrypted"**: The mod has already been protected previously.
*   **File doesn't appear in the folder**: Verify write permissions in the game's folder and that the `EncryptedMods` folder was created correctly.
*   **Format error when opening**: Normal, the file is encrypted and cannot be opened with standard tools.
*   **Android issues**: If you still have errors, ensure the game has storage permissions and you're using the latest version of the mod.

---

## 👨‍💻 About the Developer

*   **Author**: Samuelsan03
*   **Language**: 100% C#
*   **Repository**: [GitHub - Survivalcraft-2-Mod-Encryptor](https://github.com/Samuelsan03/Survivalcraft-2-Mod-Encryptor)

---

## 📜 Final Note

This mod is designed to protect your work and that of other creators. Use it ethically and always respect the copyrights of mods that aren't yours. Happy modding!
