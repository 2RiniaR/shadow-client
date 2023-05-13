using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.Shadow.Master;

namespace RineaR.Shadow.Spreadsheet
{
    /// <summary>
    ///     Google Spreadsheetからデータを取得する。
    /// </summary>
    public class FigureSheetReader
    {
        public FigureSheetReader(GoogleService googleService)
        {
            GoogleService = googleService;
        }

        public GoogleService GoogleService { get; }
        public string SheetID { get; set; }
        public string SheetName { get; set; }

        public async UniTask<IEnumerable<FigureSetting>> ReadAsync(CancellationToken cancellationToken = default)
        {
            var service = await GoogleService.GetSheetsServiceAsync(cancellationToken);
            var range = $"{SheetName}!A2:E";

            var request = service.Spreadsheets.Values.Get(SheetID, range);
            var response = await request.ExecuteAsync(cancellationToken);
            var rows = response.Values.Skip(1);
            return rows.Select(InterpretRowToItem);
        }

        private static FigureSetting InterpretRowToItem(IList<object> columns)
        {
            return new FigureSetting
            {
                id = int.Parse(columns[0] as string ?? string.Empty),
                name = columns[1] as string,
                hp = int.Parse(columns[3] as string ?? string.Empty),
                description = columns[4] as string,
            };
        }
    }
}