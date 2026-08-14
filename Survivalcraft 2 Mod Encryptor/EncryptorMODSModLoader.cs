using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Engine;
using Game;

namespace Encryptor
{
	public class EncryptorMODSModLoader : ModLoader
	{
		public override void __ModInitialize()
		{
			ModsManager.RegisterHook("OnSettingsScreenCreated", this, 0);
		}

		public override void OnSettingsScreenCreated(SettingsScreen settingsScreen, out Dictionary<ButtonWidget, Action> buttonsToAdd)
		{
			ButtonWidget encryptButton = new BevelledButtonWidget
			{
				Text = LanguageControl.Get("EncryptorMODSModLoader", 1),
				Style = ContentManager.Get<XElement>("Styles/ButtonStyle_310x60"),
				HorizontalAlignment = WidgetAlignment.Center,
				VerticalAlignment = WidgetAlignment.Center,
				Margin = new Vector2(0f, 0f)
			};

			Action onClick = () =>
			{
				ScreensManager.SwitchScreen(new EncryptorScreen(), Array.Empty<object>());
			};

			buttonsToAdd = new Dictionary<ButtonWidget, Action> { { encryptButton, onClick } };
		}
	}
}
