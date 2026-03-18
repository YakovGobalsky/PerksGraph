using R3;
using System;
using UnityEngine;

namespace Opaq.TestProject.Config {

	public class SerializatorPlayerPrefsPlayerData: IPlayerDataStorage {
		public SerializatorPlayerPrefsPlayerData (PlayerData playerData) {
			RegisterValue("coins", playerData.Coins, 0);
			//RegisterValue("gold", playerData.Gold, 0);

			//RegisterValue("level", playerData.CurrentLevel, 0);

			//playerData.SetItemsDelegate(new ItemsPlayerPrefs("item_"));
			playerData.SetItemsDelegate(new PlayerItemsStorage(this));
		}

		public void RegisterValue (string key, ReactiveProperty<int> property, int defaultValue = 0) {
			property.Value = PlayerPrefs.GetInt(key, defaultValue);

			property.Subscribe((x) => {
				PlayerPrefs.SetInt(key, x);
			});
		}
	}
}