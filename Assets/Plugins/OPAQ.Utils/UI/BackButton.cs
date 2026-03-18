using UnityEngine;
using UnityEngine.UI;
using Reflex.Attributes;

namespace Opaq.Utils.UI.BackButton {
	[RequireComponent(typeof(Button))]
	public class BackButton: MonoBehaviour, IBackButton {
		[Inject] private BackButtonManager manager;

		private void OnEnable() {
			manager.Add(this);
		}

		private void OnDisable() {
			manager.Remove(this);
		}

		public void ExecuteButtonEvent() {
			if (TryGetComponent<Button>(out var btn)) {
				btn.onClick?.Invoke();
			}
		}
	}
}