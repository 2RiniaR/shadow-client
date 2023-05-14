using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace RineaR.Shadow.Master.Spreadsheet
{
    [CreateAssetMenu(menuName = ProjectConstants.CreateAssetMenuFolder + "/Master Spreadsheet")]
    public class MasterSpreadsheet : ScriptableObject, IMasterRepository
    {
        [field: SerializeField] public GoogleService GoogleService { get; set; }
        [field: SerializeField] public MasterAssetRegistry AssetRegistry { get; set; }
        [field: SerializeField] public string SheetID { get; set; }
        [field: SerializeField] public string FigureSheetName { get; set; }
        [field: SerializeField] public string CardSheetName { get; set; }
        [field: SerializeField] public string FieldSheetName { get; set; }

        private Dictionary<string, CardSetting> _cards;
        private Dictionary<string, FieldSetting> _fields;
        private Dictionary<string, FigureSetting> _figures;

        public IEnumerable<FigureSetting> GetFigures()
        {
            return _figures.Values;
        }

        [CanBeNull]
        public FigureSetting GetFigureByID(string id)
        {
            return _figures.TryGetValue(id, out var item) ? item : null;
        }

        public IEnumerable<CardSetting> GetCards()
        {
            return _cards.Values;
        }

        [CanBeNull]
        public CardSetting GetCardByID(string id)
        {
            return _cards.TryGetValue(id, out var item) ? item : null;
        }

        public IEnumerable<FieldSetting> GetFields()
        {
            return _fields.Values;
        }

        [CanBeNull]
        public FieldSetting GetFieldByID(string id)
        {
            return _fields.TryGetValue(id, out var item) ? item : null;
        }

        public async UniTask FetchAsync(CancellationToken cancellationToken = default)
        {
            var figureSheet = new FigureSheet(GoogleService, AssetRegistry)
            {
                SheetID = SheetID,
                SheetName = FigureSheetName,
            };
            foreach (var figure in await figureSheet.ReadAsync(cancellationToken))
            {
                if (_figures.ContainsKey(figure.ID))
                {
                    Debug.LogWarning($"[{typeof(MasterSpreadsheet)}] FigureのIDが重複しています。（ID={figure.ID}）", this);
                    continue;
                }

                _figures.Add(figure.ID, figure);
            }
        }

        [ContextMenu("Print Figures")]
        public async void PrintFigures()
        {
            var figureSheetReader = new FigureSheet(GoogleService, AssetRegistry)
            {
                SheetID = SheetID,
                SheetName = FigureSheetName,
            };
            var figures = await figureSheetReader.ReadAsync();
            foreach (var figure in figures)
            {
                Debug.Log($"ID: {figure.ID}, Name: {figure.Name}, HP: {figure.Hp}, description: {figure.Description}");
            }
        }
    }
}