using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.Shadow.Master.Spreadsheet
{
    /// <summary>
    ///     Google Spreadsheetからデータを取得する。
    /// </summary>
    public class FigureSheet
    {
        private readonly MasterAssetRegistry _assetRegistry;
        private readonly GoogleService _googleService;

        public FigureSheet(GoogleService googleService, MasterAssetRegistry assetRegistry)
        {
            _assetRegistry = assetRegistry;
            _googleService = googleService;
        }

        public string SheetID { get; set; }

        public string SheetName { get; set; }

        public async UniTask<IEnumerable<FigureSetting>> ReadAsync(CancellationToken cancellationToken = default)
        {
            var sheetsService = await _googleService.GetSheetsServiceAsync(cancellationToken);
            var range = $"{SheetName}!A2:D";

            var request = sheetsService.Spreadsheets.Values.Get(SheetID, range);
            var response = await request.ExecuteAsync(cancellationToken);
            var rows = response.Values;
            return rows.Select(InterpretRowToItem);
        }

        private FigureSetting InterpretRowToItem(IList<object> columns)
        {
            var id = columns[0] as string;

            return new FigureSetting
            {
                ID = id,
                Name = columns[1] as string,
                FaceImage = _assetRegistry.FindFigureAsset(id)?.FaceImage,
                Hp = int.Parse(columns[2] as string ?? string.Empty),
                Description = columns[3] as string,
            };
        }
    }
}