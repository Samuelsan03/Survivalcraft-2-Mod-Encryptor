using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Engine;
using Engine.Graphics;
using Game;

namespace Encryptor
{
	public class EncryptorScreen : Screen
	{
		private static readonly string HeadingCode2 = "修改他人mod请获得原作者授权，否则小心出名！";
		private static readonly string EncryptedModsFolder = ModsManager.ExternalPath + "/EncryptedMods";

		private LabelWidget m_directoryLabel;
		private ListPanelWidget m_directoryList;
		private LabelWidget m_pathLabel;
		private LabelWidget m_infoLabel;
		private ButtonWidget m_upDirectoryButton;
		private ButtonWidget m_encryptButton;
		private string m_path;
		private string m_modsPathSystem;
		private bool m_listDirty;

		public EncryptorScreen()
		{
			XElement node = ContentManager.Get<XElement>("Screens/EncryptorScreen");
			LoadContents(this, node);

			m_directoryLabel = Children.Find<LabelWidget>("TopBar.Label", true);
			m_pathLabel = Children.Find<LabelWidget>("PathLabel", true);
			m_infoLabel = Children.Find<LabelWidget>("InfoLabel", true);
			m_directoryList = Children.Find<ListPanelWidget>("DirectoryList", true);
			m_upDirectoryButton = Children.Find<ButtonWidget>("UpDirectory", true);
			m_encryptButton = Children.Find<ButtonWidget>("EncryptButton", true);

			m_directoryList.ItemWidgetFactory = delegate (object item)
			{
				FileEntry entry = (FileEntry)item;

				XElement itemNode = ContentManager.Get<XElement>("Widgets/EncryptorItem");
				StackPanelWidget container = (StackPanelWidget)Widget.LoadWidget(this, itemNode, null);

				RectangleWidget icon = container.Children.Find<RectangleWidget>("EncryptorItem.Icon", true);
				LabelWidget textLabel = container.Children.Find<LabelWidget>("EncryptorItem.Text", true);
				LabelWidget detailsLabel = container.Children.Find<LabelWidget>("EncryptorItem.Details", true);

				if (entry.IsDirectory)
				{
					icon.Subtexture = TextureAtlasManager.GetSubtexture("Textures/Atlas/FolderIcon");
				}
				else
				{
					Texture2D modIconTexture = ContentManager.Get<Texture2D>("Textures/Gui/DefaultModIcon");
					icon.Subtexture = new Subtexture(modIconTexture);
				}

				textLabel.Text = entry.DisplayName;
				detailsLabel.Text = entry.IsDirectory ? string.Empty : DataSizeFormatter.Format(entry.Size, 3);

				return container;
			};

			m_directoryList.ItemClicked += delegate (object item)
			{
				FileEntry entry = item as FileEntry;
				if (entry == null) return;

				if (m_directoryList.SelectedItem == entry && entry.IsDirectory)
				{
					SetPath(entry.FullPath);
				}
			};
		}

		public override void Enter(object[] parameters)
		{
			m_modsPathSystem = null;
			SetPath(null);
			m_listDirty = true;
		}

		public override void Update()
		{
			if (m_listDirty)
			{
				m_listDirty = false;
				UpdateList();
			}

			m_directoryLabel.Text = LanguageControl.Get("EncryptorScreen", 1);
			m_encryptButton.Text = LanguageControl.Get("EncryptorScreen", 9);

			if (m_modsPathSystem == null)
			{
				m_modsPathSystem = Storage.ProcessPath(ModsManager.ModsPath, false, false).Replace('\\', '/');
			}

			string displayPath = m_path;
			if (m_path.StartsWith(m_modsPathSystem, StringComparison.OrdinalIgnoreCase))
			{
				displayPath = "Mods" + m_path.Substring(m_modsPathSystem.Length);
			}
			m_pathLabel.Text = displayPath;

			FileEntry selectedEntry = null;
			if (m_directoryList.SelectedIndex != null)
			{
				selectedEntry = m_directoryList.Items[m_directoryList.SelectedIndex.Value] as FileEntry;
			}

			// Actualizado: Se verifica IsModFile en lugar de IsScmod
			m_encryptButton.IsEnabled = selectedEntry != null && selectedEntry.IsModFile;

			m_upDirectoryButton.IsEnabled = !(m_path.Length == 2 && m_path[1] == ':');

			if (m_upDirectoryButton.IsClicked)
			{
				string parentPath = GetParentPath(m_path);
				if (!string.IsNullOrEmpty(parentPath))
				{
					SetPath(parentPath);
				}
			}

			// Actualizado: Verifica IsModFile
			if (m_encryptButton.IsClicked && selectedEntry != null && selectedEntry.IsModFile)
			{
				EncryptFile(selectedEntry.FullPath);
			}

			if (Input.Back || Input.Cancel || Children.Find<ButtonWidget>("TopBar.Back", true).IsClicked)
			{
				ScreensManager.SwitchScreen("Settings", Array.Empty<object>());
			}
		}

		private void SetPath(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				path = Storage.ProcessPath(ModsManager.ModsPath, false, false);
			}
			path = path.Replace('\\', '/');
			if (path.Length > 1 && path.EndsWith("/"))
			{
				path = path.Substring(0, path.Length - 1);
			}
			if (path != m_path)
			{
				m_path = path;
				m_listDirty = true;
			}
		}

		private string GetParentPath(string path)
		{
			if (string.IsNullOrEmpty(path))
				return null;

			if (path.Length == 2 && path[1] == ':')
				return null;

			try
			{
				string searchPath = path;
				if (path.Length == 2 && path[1] == ':')
				{
					searchPath = path + "/";
				}

				DirectoryInfo parentDir = Directory.GetParent(searchPath);
				if (parentDir != null)
				{
					return parentDir.FullName.Replace('\\', '/');
				}
			}
			catch { }

			return null;
		}

		private void UpdateList()
		{
			m_directoryList.ClearItems();

			try
			{
				string searchPath = m_path;

				if (m_path.Length == 2 && m_path[1] == ':')
				{
					searchPath = m_path + "/";
				}

				if (!Directory.Exists(searchPath))
				{
					m_infoLabel.Text = LanguageControl.Get("EncryptorScreen", 4);
					return;
				}

				List<string> directories = new List<string>(Directory.GetDirectories(searchPath));
				directories.Sort(StringComparer.OrdinalIgnoreCase);

				foreach (string dir in directories)
				{
					string dirName = Path.GetFileName(dir);
					string normalizedPath = dir.Replace('\\', '/');

					m_directoryList.AddItem(new FileEntry
					{
						Name = dirName,
						DisplayName = dirName + "/",
						FullPath = normalizedPath,
						IsDirectory = true,
						IsModFile = false,
						Size = 0
					});
				}

				List<string> files = new List<string>();
				foreach (string file in Directory.GetFiles(searchPath))
				{
					// CAMBIO PRINCIPAL: Ahora acepta .scmod y .netmod
					string ext = Path.GetExtension(file).ToLowerInvariant();
					if (ext == ".scmod" || ext == ".netmod")
					{
						files.Add(file);
					}
				}
				files.Sort(StringComparer.OrdinalIgnoreCase);

				foreach (string file in files)
				{
					FileInfo fileInfo = new FileInfo(file);
					string fileName = Path.GetFileName(file);
					string normalizedPath = file.Replace('\\', '/');

					m_directoryList.AddItem(new FileEntry
					{
						Name = fileName,
						DisplayName = fileName,
						FullPath = normalizedPath,
						IsDirectory = false,
						IsModFile = true, // Marcamos como archivo válido para encriptar
						Size = fileInfo.Length
					});
				}

				m_infoLabel.Text = m_directoryList.Items.Count == 0
					? LanguageControl.Get("EncryptorScreen", 3)
					: LanguageControl.Get("EncryptorScreen", 2);
			}
			catch (UnauthorizedAccessException)
			{
				m_infoLabel.Text = LanguageControl.Get("EncryptorScreen", 5);
			}
			catch (Exception ex)
			{
				DialogsManager.ShowDialog(null, new MessageDialog(LanguageControl.Error, ex.Message, LanguageControl.Ok, null, null));
			}
		}

		private void EncryptFile(string filePath)
		{
			try
			{
				byte[] originalData;
				using (FileStream srcStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
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
						LanguageControl.Get("Usual", "warning"),
						LanguageControl.Get("EncryptorScreen", 6),
						LanguageControl.Ok, null, null));
					return;
				}

				if (!Storage.DirectoryExists(EncryptedModsFolder))
				{
					Storage.CreateDirectory(EncryptedModsFolder);
				}

				string fileName = Path.GetFileName(filePath);
				string baseName = Path.GetFileNameWithoutExtension(fileName);

				// CAMBIO PRINCIPAL: Obtener la extensión original (.scmod o .netmod)
				string originalExtension = Path.GetExtension(filePath);

				string newFileName = $"{baseName} (Encrypted){originalExtension}";
				string destPath = Storage.CombinePaths(EncryptedModsFolder, newFileName);

				int counter = 1;
				while (Storage.FileExists(destPath))
				{
					newFileName = $"{baseName} (Encrypted)({counter}){originalExtension}";
					destPath = Storage.CombinePaths(EncryptedModsFolder, newFileName);
					counter++;
				}

				// Lógica de encriptación (Intercalo de bytes: Pares primero, Impares después)
				byte[] encryptedData = new byte[originalData.Length + headerBytes.Length];
				Array.Copy(headerBytes, 0, encryptedData, 0, headerBytes.Length);
				int destIndex = headerBytes.Length;

				// Copia indices pares (0, 2, 4...)
				for (int i = 0; i < originalData.Length; i += 2)
					encryptedData[destIndex++] = originalData[i];

				// Copia indices impares (1, 3, 5...)
				for (int i = 1; i < originalData.Length; i += 2)
					encryptedData[destIndex++] = originalData[i];

				string destSystemPath = Storage.GetSystemPath(destPath);
				using (FileStream destStream = new FileStream(destSystemPath, FileMode.Create, FileAccess.Write, FileShare.None))
					destStream.Write(encryptedData, 0, encryptedData.Length);

				DialogsManager.ShowDialog(null, new MessageDialog(
					LanguageControl.Success,
					string.Format(LanguageControl.Get("EncryptorScreen", 7), destPath),
					LanguageControl.Ok, null, null));

				Log.Information($"Mod encrypted: {baseName} -> {destPath}");
			}
			catch (Exception ex)
			{
				DialogsManager.ShowDialog(null, new MessageDialog(
					LanguageControl.Error,
					string.Format(LanguageControl.Get("EncryptorScreen", 8), ex.Message),
					LanguageControl.Ok, null, null));
				Log.Error($"Encryption error: {ex}");
			}
		}

		private class FileEntry
		{
			public string Name;
			public string DisplayName;
			public string FullPath;
			public bool IsDirectory;
			public bool IsModFile; // Cambiado de IsScmod a IsModFile para soportar ambos
			public long Size;
		}
	}
}
