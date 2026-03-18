using UnityEngine;

namespace Opaq.Utils {
	public class TransitionScreenEffect: MonoBehaviour {
		public virtual void ShowEffect (System.Action onDone, System.Action onComplete = null) {
			onDone?.Invoke();
			onComplete?.Invoke();
		}
	}
}