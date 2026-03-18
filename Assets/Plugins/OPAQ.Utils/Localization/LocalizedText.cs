using UnityEngine;
using TMPro;

namespace Opaq.Utils.Localization {
	[RequireComponent(typeof(TMP_Text))]
	public class LocalizedText: LocalizedBehaviour {
		[SerializeField] private string key;

		private TMP_Text text;

		private void Awake () {
			text = GetComponent<TMP_Text>();
		}

		protected override void UpdateLocalizedInfo (Locale locale) {
			text.text = locale.Localize(key);
		}
	}
}