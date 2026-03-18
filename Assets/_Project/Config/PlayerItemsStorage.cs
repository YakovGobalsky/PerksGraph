using R3;
using System.Collections.Generic;

namespace Opaq.TestProject.Config {
	public class PlayerItemsStorage: IPlayerItemsDelegate {
		private string keyPrefix;
		private IPlayerDataStorage dataStorage;
		private Dictionary<string, ReactiveProperty<int>> reactivesCache = new();

		public PlayerItemsStorage(IPlayerDataStorage dataStorage, string keyPrefix = "item_") {
			this.keyPrefix = keyPrefix;
			this.dataStorage = dataStorage;
		}

		public ReactiveProperty<int> GetItemCount (string itemId) {
			if (reactivesCache.TryGetValue(itemId, out var count)) {
				return count;
			}
			var r = new ReactiveProperty<int>();
			reactivesCache.Add(itemId, r);

			dataStorage.RegisterValue($"{keyPrefix}{itemId}", r);
			return r;
		}
	}
}