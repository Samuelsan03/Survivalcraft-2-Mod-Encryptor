using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Engine;
using Game;

namespace Encryptor
{
	public class EncryptorMODSModLoader : ModLoader
	{
		// Usar ExternalPath + "/EncryptedMods" (app:/EncryptedMods)
		private static readonly string EncryptedModsFolder = ModsManager.ExternalPath + "/EncryptedMods";

		private static readonly string HeadingCode2 = "修改他人mod请获得原作者授权，否则小心出名！";

		public override void __ModInitialize()
		{
			ModsManager.RegisterHook("OnSettingsScreenCreated", this, 0);
		}

		public override void OnSettingsScreenCreated(SettingsScreen settingsScreen, out Dictionary<ButtonWidget, Action> buttonsToAdd)
		{
			ButtonWidget encryptButton = new BevelledButtonWidget
			{
				Text = LanguageControl.Get("Encryptor", "Button"),
				Style = ContentManager.Get<System.Xml.Linq.XElement>("Styles/ButtonStyle_310x60"),
				HorizontalAlignment = WidgetAlignment.Center,
				VerticalAlignment = WidgetAlignment.Center,
				Margin = new Vector2(0f, 0f)
			};

			Action onClick = () =>
			{
				List<ModEntity> mods = ModsManager.ModListAll;
				ListSelectionDialog dialog = new ListSelectionDialog(
					LanguageControl.Get("Encryptor", "SelectTitle"),
					mods,
					50f,
					(item) =>
					{
						ModEntity mod = item as ModEntity;
						if (mod == null) return null;
						return new LabelWidget
						{
							Text = mod.modInfo?.Name ?? Storage.GetFileName(mod.ModFilePath),
							HorizontalAlignment = WidgetAlignment.Center,
							VerticalAlignment = WidgetAlignment.Center,
							Color = mod.IsDisabled ? Color.Gray : Color.White
						};
					},
					(selected) =>
					{
						ModEntity mod = selected as ModEntity;
						if (mod != null) EncryptMod(mod);
					}
				);
				DialogsManager.ShowDialog(null, dialog);
			};

			buttonsToAdd = new Dictionary<ButtonWidget, Action> { { encryptButton, onClick } };
		}

		private void EncryptMod(ModEntity mod)
		{
			try
			{
				if (string.IsNullOrEmpty(mod.ModFilePath) || !Storage.FileExists(mod.ModFilePath))
				{
					DialogsManager.ShowDialog(null, new MessageDialog(
						LanguageControl.Get("Encryptor", "ErrorTitle"),
						LanguageControl.Get("Encryptor", "FileNotFound"),
						LanguageControl.Ok, null, null));
					return;
				}

				// Crear la carpeta EncryptedMods en la raíz (app:/EncryptedMods)
				if (!Storage.DirectoryExists(EncryptedModsFolder))
				{
					Storage.CreateDirectory(EncryptedModsFolder);
				}

				string fileName = Storage.GetFileName(mod.ModFilePath);
				string baseName = Storage.GetFileNameWithoutExtension(fileName);
				string ext = Storage.GetExtension(fileName);

				if (ext != ".scmod")
				{
					DialogsManager.ShowDialog(null, new MessageDialog(
						LanguageControl.Get("Encryptor", "ErrorTitle"),
						LanguageControl.Get("Encryptor", "InvalidExtension"),
						LanguageControl.Ok, null, null));
					return;
				}

				string newFileName = $"{baseName} (Encrypted){ext}";
				string destPath = Storage.CombinePaths(EncryptedModsFolder, newFileName);

				int counter = 1;
				while (Storage.FileExists(destPath))
				{
					newFileName = $"{baseName} (Encrypted)({counter}){ext}";
					destPath = Storage.CombinePaths(EncryptedModsFolder, newFileName);
					counter++;
				}

				byte[] originalData;
				using (Stream srcStream = Storage.OpenFile(mod.ModFilePath, OpenFileMode.Read))
				{
					originalData = new byte[srcStream.Length];
					srcStream.ReadExactly(originalData);
				}

				byte[] headerBytes = Encoding.UTF8.GetBytes(HeadingCode2);
				bool hasHeader = true;
				for (int i = 0; i < headerBytes.Length; i++)
				{
					if (i >= originalData.Length || originalData[i] != headerBytes[i])
					{
						hasHeader = false;
						break;
					}
				}

				if (hasHeader)
				{
					DialogsManager.ShowDialog(null, new MessageDialog(
						LanguageControl.Get("Encryptor", "InfoTitle"),
						LanguageControl.Get("Encryptor", "AlreadyEncrypted"),
						LanguageControl.Ok, null, null));
					return;
				}

				byte[] encryptedData = new byte[originalData.Length + headerBytes.Length];
				Array.Copy(headerBytes, 0, encryptedData, 0, headerBytes.Length);
				int destIndex = headerBytes.Length;
				for (int i = 0; i < originalData.Length; i += 2)
					encryptedData[destIndex++] = originalData[i];
				for (int i = 1; i < originalData.Length; i += 2)
					encryptedData[destIndex++] = originalData[i];

				using (Stream destStream = Storage.OpenFile(destPath, OpenFileMode.Create))
					destStream.Write(encryptedData, 0, encryptedData.Length);

				DialogsManager.ShowDialog(null, new MessageDialog(
					LanguageControl.Get("Encryptor", "SuccessTitle"),
					string.Format(LanguageControl.Get("Encryptor", "SuccessMessage"), destPath),
					LanguageControl.Ok, null, null));

				Log.Information(string.Format(LanguageControl.Get("Encryptor", "LogSuccess"), mod.modInfo?.Name ?? fileName, destPath));
			}
			catch (Exception ex)
			{
				DialogsManager.ShowDialog(null, new MessageDialog(
					LanguageControl.Get("Encryptor", "ErrorTitle"),
					string.Format(LanguageControl.Get("Encryptor", "ErrorMessage"), ex.Message),
					LanguageControl.Ok, null, null));
				Log.Error(string.Format(LanguageControl.Get("Encryptor", "LogError"), ex));
			}
		}
	}
}
