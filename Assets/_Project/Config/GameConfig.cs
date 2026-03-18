using UnityEngine;
using R3;

namespace Opaq.TestProject {
	[System.Serializable]
	public class GameConfig {
		public SerializableReactiveProperty<float> SoundVolume = new(0.5f);
		public SerializableReactiveProperty<float> MusicVolume = new(0.5f);

		//public SerializableReactiveProperty<bool> IsTermsAccepted = new(false);

		public SerializableReactiveProperty<string> Language = new("");

	}
}