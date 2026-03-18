using System.Collections.Generic;
using UnityEngine;

namespace Opaq.Utils.Localization {
	[CreateAssetMenu(menuName = "OpaqUtils/LocalizationLocale")]
	public class Locale: ScriptableObject {
		[SerializeField] private string language;
		[SerializeField] private SystemLanguage systemLanguage;

		[SerializeField] private List<string> keys;
		[SerializeField] private List<string> values;

		public SystemLanguage GetLanguage () => systemLanguage;
		public string GetLanguageId () => language;

		private Dictionary<string, string> dictionary = null;

		public void CacheValues () {
			dictionary = new Dictionary<string, string>(keys.Count);
			for (int i = 0; i < keys.Count; i++) {
				dictionary.Add(keys[i], values[i]);
			}
		}

		public string Localize (string key) {
			if (dictionary.ContainsKey(key)) {
				return dictionary[key];
			} else {
				Debug.LogWarning($"Localization: key {key} not found in language {language}");
				return key;
			}
		}

#if UNITY_EDITOR
		public void AddKeyValue (string key, string value) {
			keys.Add(key);
			values.Add(value);
		}

		public void ClearKeysValues () {
			keys.Clear();
			values.Clear();
		}
#endif
	}
}