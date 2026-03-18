using System.Collections.Generic;
using UnityEngine;

namespace Opaq.Utils.UI.BackButton {
	public class BackButtonManager: MonoBehaviour {
		private readonly List<IBackButton> buttons = new ();

		public void Add(IBackButton btn) {
			buttons.Add(btn);
		}

		public void Remove(IBackButton btn) {
			buttons.Remove(btn);
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Escape) && buttons.Count > 0) {
				buttons[buttons.Count - 1].ExecuteButtonEvent();
			}
		}
	}
}