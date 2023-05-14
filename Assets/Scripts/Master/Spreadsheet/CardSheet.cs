using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.Shadow.Master.Spreadsheet
{
    /// <summary>
    ///     Google Spreadsheetからデータを取得する。
    /// </summary>
    public class CardSheet
    {
        public CardSheet(GoogleService googleService)
        {
            GoogleService = googleService;
        }

        public GoogleService GoogleService { get; }
        public string SheetID { get; set; }

        public string SheetName { get; set; }

        public async UniTask<IEnumerable<CardSetting>> ReadAsync(CancellationToken cancellationToken = default)
        {
            var sheetsService = await GoogleService.GetSheetsServiceAsync(cancellationToken);
            var range = $"{SheetName}!A2:E";

            var request = sheetsService.Spreadsheets.Values.Get(SheetID, range);
            var response = await request.ExecuteAsync(cancellationToken);
            var rows = response.Values.Skip(1);
            return rows.Select(InterpretRowToItem);
        }

        private static CardSetting InterpretRowToItem(IList<object> columns)
        {
            return new CardSetting
            {
                ID = columns[0] as string,
                Name = columns[1] as string,
            };
        }
    }
}