using System.IO;
using UnityEngine;

namespace Opaq.TestProject.Config {
	public class SerializatorJSON<T> where T : new() {
		private T data;
		private string fileName;

		private string FileName => $"{Application.persistentDataPath}/{fileName}.json";

		public SerializatorJSON (ref T data, string file) {
			this.fileName = file;
			this.data = data = LoadData();

			var beh = new GameObject($"json_{this.fileName}").AddComponent<SerializatorJSONBehaviaour>();
			beh.OnFocusLost += SaveData;
			//beh.Init(ref data, fileName);
			//beh is responsible for his lifetime

			SaveData();
		}

		private T LoadData () {
			try {
				return JsonUtility.FromJson<T>(File.ReadAllText(FileName));
			} catch {

			}
			return new T();
		}

		private void SaveData () {
			try {
				var text = JsonUtility.ToJson(data);
				File.WriteAllText(FileName, text);
			} catch {

			}
		}

	}
}