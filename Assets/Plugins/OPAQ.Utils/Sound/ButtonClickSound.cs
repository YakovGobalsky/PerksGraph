using UnityEngine;
using UnityEngine.EventSystems;
using Reflex.Attributes;

namespace Opaq.Utils {
	public class ButtonClickSound: MonoBehaviour, IPointerClickHandler {
		[SerializeField] private AudioClip customClickSound;

		[Inject] private SoundManager soundManager;

		public void OnPointerClick(PointerEventData eventData) {
			if (customClickSound) {
				soundManager?.PlayOnce(customClickSound);
			} else {
				soundManager?.PlayClickSound();
			}
		}
	}
}