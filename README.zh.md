# Survivalcraft 2 Mod Encryptor

## 📖 描述

**Survivalcraft 2 Mod Encryptor** 是一个迷你模组，它恢复了**加固（Reinforcing）**功能。该功能允许您加密 `.scmod` 文件，为您的模组增添额外的安全保护层。使用后，您的文件将受到保护，无法再作为常规 ZIP 压缩包解压，有助于防止他人窃取或未经授权提取您的模组资源。

这项实用功能在 Survivalcraft **2.3** 版本中可用，但在 **2.4** 版本（API 1.8 和 1.9）中无故被移除。此迷你模组让您能够恢复这一功能，从而无需为了加密而依赖旧版本游戏。

**重要提示**：与旧版将文件保存在 `ModsCache` 文件夹的系统不同，本改进版创建了一个名为 `EncryptedMods` 的**独立**文件夹。这使您的原始模组和加密模组完美分离，保持井井有条。

### ✨ 主要特点

*   **一键加密**：直接从游戏设置菜单保护任何已安装的模组。
*   **独立文件夹**：加密文件保存在 `EncryptedMods` 中，与原始模组和缓存分离。
*   **完全兼容移动设备**：修复了 Android 上的文件夹访问错误，现在在手机上运行完美。
*   **界面简洁**：直观的对话框引导您完成整个过程。
*   **防重复**：自动命名文件以避免覆盖其他文件。
*   **智能检测**：识别模组是否已加密，避免不必要的处理。
*   **错误处理**：清晰的消息提示帮助您解决任何问题。

---

## 📋 使用指南

### 步骤 1：进入设置菜单
<img width="1912" height="985" alt="image" src="https://github.com/user-attachments/assets/cdb2f24b-eb72-40cd-bc39-6918dcc0f7e9" />

在游戏主屏幕中，选择 **"CONFIGURACION"（设置）** 进入调整选项。

### 步骤 2：找到加密按钮
<img width="1917" height="972" alt="image" src="https://github.com/user-attachments/assets/d28ea100-ef2a-47cd-a57d-8d2a8f9d1ebc" />

在设置菜单中，找到 **"ENCRIPTAR MOD"（加密模组）** 选项并点击它。

### 步骤 3：选择要加密的模组
<img width="1917" height="927" alt="image" src="https://github.com/user-attachments/assets/e076abb5-32a5-4d34-9846-9adaf5b1076b" />

将显示您所有已安装模组的列表。选择您想要保护的那个。

### 步骤 4：确认和加密
<img width="1917" height="967" alt="image" src="https://github.com/user-attachments/assets/2c76901b-2e6b-45e6-8352-63e814037ee9" />
<img width="1352" height="757" alt="image" src="https://github.com/user-attachments/assets/02abf2e8-b14c-49b8-a543-9e390d896da6" />

完成后，您将看到确认消息：**"EL MOD SE HA ENCRIPTADO CORRECTAMENTE."（模组已成功加密。）**
加密文件将保存到游戏目录下的 `EncryptedMods` 文件夹中。

### 步骤 5：找到加密文件
<img width="792" height="757" alt="image" src="https://github.com/user-attachments/assets/42958f5c-5da8-42f8-83b4-66f11d0d20c0" />
<img width="788" height="761" alt="image" src="https://github.com/user-attachments/assets/c1130fc2-9055-43d9-9e47-301d7caafcb1" />

您可以在以下路径找到加密的模组：
```
📁 EncryptedMods
  └── [2.4]Cloud Backpack (Encrypted).scmod
```

**存储系统说明**：
- **2.3 版本（旧版）**：将文件保存在 `ModsCache` 中。
- **使用此模组的 2.4 版本（新版）**：将文件保存在 `EncryptedMods`（独立文件夹）中。

这意味着您的原始模组保留在 `Mods` 文件夹中，加密文件单独保存，并且不会干扰游戏缓存。

### ⚠️ 关于文件的重要提示
<img width="1107" height="757" alt="image" src="https://github.com/user-attachments/assets/14f915d2-fa8a-44bb-a796-abac168b0b60" />

**如果看到此消息，请不要惊慌！**
当您直接尝试打开加密的 `.scmod` 文件时，可能会看到：
> "文件格式未知或已损坏"

**这不是错误**，而是加密的正常运作。该文件受到保护，其结构被有意混淆，因此标准查看器无法读取。但是，它对于兼容的模组加载器仍然完全可用。

---

## 💡 使用此加密器的优势

*   **保护您的作品**：防止他人未经许可修改或窃取您模组的资源。
*   **保持完整性**：确保您的模组按您的设计运行。
*   **易于使用**：无需技术知识即可保护您的文件。
*   **安全**：原始文件不会被修改，只创建加密副本。
*   **条理清晰**：`EncryptedMods` 文件夹使您的受保护文件保持分离和有序。
*   **方便**：恢复丢失的功能，无需切换游戏版本。
*   **无冲突**：不使用 `ModsCache`，避免与其他游戏系统的潜在冲突。
*   **在 Android 上功能完整**：已修复阻止移动设备创建文件夹的权限错误。

---

## 🔧 移动设备错误修复

在之前的版本中，尝试在 Android 上加密会导致访问错误：
```
错误：拒绝访问 "app:/EncryptedMods"。
```
这是因为 `app:/EncryptedMods` 路径在游戏文件系统中未被正确识别。

**此版本解决了该问题**，通过 `ModsManager.ExternalPath + "/EncryptedMods"` 使用正确的路径，确保文件夹在正确的位置创建并拥有必要的权限。现在，移动玩家可以毫无问题地加密他们的模组。

---

## ❓ 故障排除

*   **"文件未找到"**：确保所选模组存在于 `Mods` 文件夹中。
*   **"扩展名无效"**：只能加密 `.scmod` 文件。
*   **"已加密"**：该模组之前已被保护。
*   **文件夹中没有文件**：检查游戏文件夹的写入权限，并确保 `EncryptedMods` 文件夹已正确创建。
*   **打开时格式错误**：正常现象，文件已加密，无法使用标准工具打开。
*   **Android 问题**：如果仍有错误，请确保游戏具有存储权限，并且您正在使用最新版本的模组。

---

## 👨‍💻 关于开发者

*   **作者**：Samuelsan03
*   **语言**：100% C#
*   **仓库**：[GitHub - Survivalcraft-2-Mod-Encryptor](https://github.com/Samuelsan03/Survivalcraft-2-Mod-Encryptor)

---

## 📜 最后说明

此模组旨在保护您和其他创作者的作品。请合乎道德地使用它，并始终尊重不属于您的模组的版权。创作愉快！
