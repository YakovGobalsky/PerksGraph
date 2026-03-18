using UnityEngine;
using R3;

namespace Opaq.TestProject {
	[System.Serializable]
	public class GameConfig {
		public readonly SerializableReactiveProperty<float> SoundVolume = new(0.5f);
		public readonly SerializableReactiveProperty<float> MusicVolume = new(0.5f);

		//public readonly SerializableReactiveProperty<bool> IsTermsAccepted = new(false);

		public readonly SerializableReactiveProperty<string> Language = new("");
	}
}