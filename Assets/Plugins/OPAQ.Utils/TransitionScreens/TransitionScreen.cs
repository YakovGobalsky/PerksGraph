using UnityEngine;

namespace Opaq.Utils {
	public class TransitionScreen: MonoBehaviour {
		[SerializeField] private GameObject overlay;
		[SerializeField] private TransitionScreenEffect[] effects;

		public void ShowTransition (System.Action onDone, System.Action onComplete = null, int transitionIndex = 0) {
			overlay.SetActive(true);
			effects[transitionIndex]?.ShowEffect(
				onDone: onDone,
				onComplete: () => {
					overlay.SetActive(false);
					onComplete?.Invoke();
				}
			);
		}
	}
}