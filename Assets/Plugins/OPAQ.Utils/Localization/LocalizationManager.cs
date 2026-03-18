using System.Collections.Generic;
using UnityEngine;

using R3;

using Opaq.Utils.Sheets;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Opaq.Utils.Localization {
	[System.Serializable]
	public class LanguageSheetImportInfo: SheetInfo {
		public int StartCol = 1;
		public int StartRow = 0;
		public bool HasCustomIds = false;
		public string CustomIdPrefix = "";
	}

	[CreateAssetMenu(menuName = "OpaqUtils/LocalizationManager")]
	public class LocalizationManager: SheetParser<LanguageSheetImportInfo> {
		[SerializeField] private Locale[] locales;

		public ReactiveProperty<Locale> Locale { get; private set; } = new ReactiveProperty<Locale>();

		public IEnumerable<Locale> GetLocales () => locales;

		public string Localize (string key) => Locale?.Value?.Localize(key) ?? "";

		public void Init (string lang) {
			foreach (var locale in locales) {
				locale.CacheValues();
			}

			var startLocale = locales[0];
			if (string.IsNullOrEmpty(lang)) {
				foreach (var locale in locales) {
					if (Application.systemLanguage == locale.GetLanguage()) {
						startLocale = locale;
						break;
					}
				}
			} else {
				foreach (var locale in locales) {
					if (lang == locale.GetLanguageId()) {
						startLocale = locale;
						break;
					}
				}
			}

			Locale.Value = startLocale;
		}

		public void NextLocale () {
			for (int i = 1; i < locales.Length; i++) {
				if (locales[i - 1] == Locale.Value) {
					Locale.Value = locales[i];
					return;
				}
				Locale.Value = locales[0];
			}
		}

		public void PrevLocale () {
			for (int i = 0; i < locales.Length - 1; i++) {
				if (locales[i + 1] == Locale.Value) {
					Locale.Value = locales[i];
					return;
				}
				Locale.Value = locales[locales.Length - 1];
			}
		}

#if UNITY_EDITOR
		[SerializeField] private LanguageSheetImportInfo[] _sheets;

		[ContextMenu("LoadGoogleSheets")]
		public void ReloadSheets () {
			LoadGoogleSheetsImpl();
		}

		public override IEnumerable<LanguageSheetImportInfo> Sheets => _sheets;

		public override void ParseSheet (LanguageSheetImportInfo sheetInfo, string[,] sheet) {
			Debug.Log($"Parse sheet {sheet}");
			Dictionary<Locale, int> cols = new Dictionary<Locale, int>();

			for (int x = sheetInfo.StartCol; x < sheet.GetLength(0); x++) {
				var val = sheet[x, sheetInfo.StartRow];
				foreach (var locale in locales) {
					if (locale.GetLanguageId() == val) {
						cols.Add(locale, x);
					}
				}
			}

			for (int y = sheetInfo.StartRow + 1; y < sheet.GetLength(1); y++) {
				string key;
				if (sheetInfo.HasCustomIds) {
					key = GetCustomIndex(sheetInfo.CustomIdPrefix, y);
				} else {
					key = sheet[sheetInfo.StartCol, y];
				}

				foreach (var pair in cols) {
					pair.Key.AddKeyValue(key, sheet[pair.Value, y]);
				}
			}
		}

		public override void OnParseBegin () {
			foreach (var locale in locales) {
				locale.ClearKeysValues();
			}
		}

		public override void OnParseEnd () {
			foreach (var locale in locales) {
				EditorUtility.SetDirty(locale);
			}
			Debug.Log("Localization::ParseFinished");
		}
#endif
	}
}