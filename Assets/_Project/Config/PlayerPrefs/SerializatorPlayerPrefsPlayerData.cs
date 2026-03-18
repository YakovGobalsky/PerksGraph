using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Opaq.TestProject.Config {
	public class SerializatorPlayerPrefsPlayerData: IPlayerItemsDelegate, IDisposable {
		private Dictionary<string, ReactiveProperty<int>> reactivesCache = new();

		public SerializatorPlayerPrefsPlayerData (PlayerData playerData) {
			RegisterValue("coins", playerData.Coins, 0);
			//RegisterValue("gold", playerData.Gold, 0);

			//RegisterValue("level", playerData.CurrentLevel, 0);

			//playerData.SetItemsDelegate(new ItemsPlayerPrefs("item_"));
			playerData.SetItemsDelegate(this);
		}

		~SerializatorPlayerPrefsPlayerData () {
			//clean cache?
		}

		public void Dispose() {
			//clean cache?
		}

		public ReactiveProperty<int> GetItemCount (string itemId) {
			if (reactivesCache.TryGetValue(itemId, out var count)) {
				return count;
			}
			var r = new ReactiveProperty<int>();
			reactivesCache.Add(itemId, r);

			RegisterValue($"item_{itemId}", r);
			return r;
		}

		//		case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
		//			PlayerPrefs.SetInt($"item_{e.NewItem.Key}", e.NewItem.Value);
		//			break;

		private void RegisterValue (string key, ReactiveProperty<int> property, int defaultValue = 0) {
			property.Value = PlayerPrefs.GetInt(key, defaultValue);

			property.Subscribe((x) => {
				PlayerPrefs.SetInt(key, x);
			});
		}
	}
}