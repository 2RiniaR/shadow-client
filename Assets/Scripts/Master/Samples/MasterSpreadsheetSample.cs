using RineaR.Shadow.Spreadsheet;
using UnityEngine;

namespace RineaR.Shadow.Master.Samples
{
    public class MasterSpreadsheetSample : MonoBehaviour
    {
        public GoogleService googleService;

        [ContextMenu("Run")]
        private async void Test()
        {
            var spreadsheet = new MasterSpreadsheet
            {
                GoogleService = googleService,
            };

            await spreadsheet.FetchAsync(destroyCancellationToken);
            var figures = spreadsheet.GetFigures();

            foreach (var figure in figures)
            {
                Debug.Log($"ID: {figure.id}, Name: {figure.name}, HP: {figure.hp}, description: {figure.description}");
            }
        }
    }
}