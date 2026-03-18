using UnityEngine;

using R3;

namespace Opaq.TestProject {
	public interface IPlayerItemsDelegate {
		ReactiveProperty<int> GetItemCount (string itemId);

		//int GetItemCount (string itemId);
		//bool TrySpendItem (string itemId);
		//void AddItem (string itemId, uint cnt);
	}
}