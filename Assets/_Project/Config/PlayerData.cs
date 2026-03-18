using R3;

namespace Opaq.TestProject {
	[System.Serializable]
	public class PlayerData {
		public readonly SerializableReactiveProperty<int> Coins = new(0);

		private IPlayerItemsDelegate _itemsDelegate;
		public void SetItemsDelegate (IPlayerItemsDelegate itemsDelegate) => _itemsDelegate = itemsDelegate;

		public ReactiveProperty<int> GetItemCount (string itemId) => _itemsDelegate?.GetItemCount(itemId);
	}
}