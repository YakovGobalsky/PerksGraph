using UnityEngine;
using Reflex.Attributes;

namespace Opaq.Utils.UI {
	[RequireComponent(typeof(RectTransform))]
	public class SafeArea: MonoBehaviour {
		private RectTransform rectTransform;

		[Inject] private UITools uiTools;

		private void Awake() {
			rectTransform = GetComponent<RectTransform>();
		}

		private void OnEnable () {
			uiTools.OnSafeAreaChanged += UpdateTransform;
			UpdateTransform(uiTools.activeSafeArea);
		}

		private void OnDisable () {
			uiTools.OnSafeAreaChanged -= UpdateTransform;
		}

		private void UpdateTransform(Rect safeArea) {
			var anchorMin = safeArea.position;
			var anchorMax = safeArea.position + safeArea.size;
			anchorMin.x /= Screen.width;
			anchorMin.y /= Screen.height;
			anchorMax.x /= Screen.width;
			anchorMax.y /= Screen.height;
			rectTransform.anchorMin = anchorMin;
			rectTransform.anchorMax = anchorMax;
		}
	}
}