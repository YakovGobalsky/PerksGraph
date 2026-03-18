using System.Collections.Generic;
using UnityEngine;

namespace Opaq.Utils.Sheets {
	[System.Serializable]
	public class SheetInfo {
		public string sheetId;
		public string sheetName;
	}

	public abstract class SheetParser<T>: ScriptableObject where T : SheetInfo {
#if UNITY_EDITOR
		[SerializeField] private TextAsset credentialJSON;

		public virtual IEnumerable<T> Sheets { get; }

		public string GetCustomIndex (string prefix, int y) => prefix + y.ToString();

		public virtual void OnParseBegin () {
		}

		public virtual void OnParseEnd () {
		}

		public virtual void ParseSheet (T sheetInfo, string[,] sheet) {
		}

		public async void LoadGoogleSheetsImpl () {
			OnParseBegin();

			foreach (var sheetInfo in Sheets) {
				var loader = new SpreadsheetsLoader(credentialJSON.text, sheetInfo.sheetId);
				var values = await loader.GetValues(sheetInfo.sheetName);
				int w = values.Values.Count;
				int h = values.Values[0].Count;

				foreach (var line in values.Values) {
					h = Mathf.Max(h, line.Count);
				}
				string[,] sheet = new string[w, h];
				//Debug.Log($"Sheet: {w}x{h}");

				for (int x = 0; x < w; x++) {
					int y;
					for (y = 0; y < values.Values[x].Count; y++) {
						var val = values.Values[x][y]?.ToString() ?? "";
						sheet[x, y] = val;
					}

					for (; y < h; y++) {
						sheet[x, y] = "";
					}
				}
				ParseSheet(sheetInfo, sheet);
			}

			OnParseEnd();
		}

#endif

	}
}