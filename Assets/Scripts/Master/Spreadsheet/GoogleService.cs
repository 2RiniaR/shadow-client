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
        [field: SerializeField] public string ClientID { get; set; }

        [field: SerializeField] public string ClientSecret { get; set; }

        public async UniTask<SheetsService> GetSheetsServiceAsync(CancellationToken cancellationToken = default)
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = ClientID,
                    ClientSecret = ClientSecret,
                },
                new[] { SheetsService.Scope.SpreadsheetsReadonly },
                "user",
                cancellationToken
            );

            return new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
            });
        }
    }
}