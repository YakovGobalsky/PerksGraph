using UnityEngine;
using Reflex.Attributes;

using Opaq.TestProject;

namespace Opaq {
	public class DbgTestConfig: MonoBehaviour {
		[Inject] private PlayerData playerData;

		public void AddCoins (int amount) => playerData.Coins.Value += amount;

		//public void AddGold (int amount) => _playerData.Gold.Value += amount;

	}
}