using UnityEngine;

namespace Opaq.TestProject.Config {
	public class SerializatorJSONBehaviaour: MonoBehaviour {
		public event System.Action OnFocusLost;

		private void Awake () {
			DontDestroyOnLoad(gameObject);
		}

		private void OnApplicationFocus (bool focus) {
			if (!focus) {
				OnFocusLost?.Invoke();
			}
		}

		private void OnApplicationQuit () {
			OnFocusLost?.Invoke();
		}
	}
}