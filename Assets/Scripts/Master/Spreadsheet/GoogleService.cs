using System.Threading;
using Cysharp.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using UnityEngine;

namespace RineaR.Shadow.Master.Spreadsheet
{
    [CreateAssetMenu(menuName = Constants.CreateAssetMenuFolder + "/Google Service")]
    public class GoogleService : ScriptableObject
    {
        [field: SerializeField] public string CredentialPath { get; set; }

        public async UniTask<SheetsService> GetSheetsServiceAsync(CancellationToken cancellationToken = default)
        {
            var json = await Resources.LoadAsync<TextAsset>(CredentialPath);
            var credential = GoogleCredential.FromJson(json.ToString());

            return new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
            });
        }
    }
}