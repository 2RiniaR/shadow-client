using System.Threading;
using Cysharp.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using UnityEngine;

namespace RineaR.Shadow.Spreadsheet
{
    [CreateAssetMenu(menuName = "shadow/Google Service")]
    public class GoogleService : ScriptableObject
    {
        [Header("Credentials")]
        public string clientId;

        public string clientSecret;

        public async UniTask<SheetsService> GetSheetsServiceAsync(CancellationToken cancellationToken = default)
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
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