using Cysharp.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace Opaq.Utils.Sheets {
	public class SpreadsheetsLoader {
		private SheetsService sheetsService;
		private string spreadsheetId;

		public SpreadsheetsLoader (string credentialsJSON, string spreadSheetId) {
			spreadsheetId = spreadSheetId;
			sheetsService = new SheetsService(new BaseClientService.Initializer() {
				HttpClientInitializer = GetCredentialsFromFile(credentialsJSON)
			});
		}

		private GoogleCredential GetCredentialsFromFile (string json) {
			return GoogleCredential.FromJson(json).CreateScoped(SheetsService.Scope.Spreadsheets);
		}

		public async UniTask<ValueRange> GetValues (string sheetName) {
			var range = $"{sheetName}!A1:Z";
			var request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
			request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;

			ValueRange response = null;
			try {
				response = await request.ExecuteAsync().AsUniTask();
			} catch (System.Exception e) {
				Debug.LogError(e.Message);
			}

			return response;
		}
	}
}