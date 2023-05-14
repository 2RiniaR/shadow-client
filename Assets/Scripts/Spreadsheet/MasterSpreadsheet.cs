using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.Shadow.Master;
using UnityEngine;

namespace RineaR.Shadow.Spreadsheet
{
    [CreateAssetMenu(menuName = ProjectConstants.CreateAssetMenuFolder + "/Master Spreadsheet")]
    public class MasterSpreadsheet : ScriptableObject, IMasterRepository
    {
        [field: SerializeField] public GoogleService GoogleService { get; set; }
        [field: SerializeField] public string SheetID { get; set; }
        [field: SerializeField] public string FigureSheetName { get; set; }

        private IEnumerable<CardSetting> _cards;
        private IEnumerable<FieldSetting> _fields;
        private IEnumerable<FigureSetting> _figures;

        public IEnumerable<FigureSetting> GetFigures()
        {
            return _figures;
        }

        public FigureSetting GetFigureByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CardSetting> GetCards()
        {
            return _cards;
        }

        public CardSetting GetCardByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FieldSetting> GetFields()
        {
            return _fields;
        }

        public FieldSetting GetFieldByID(int id)
        {
            throw new NotImplementedException();
        }

        public async UniTask FetchAsync(CancellationToken cancellationToken = default)
        {
            var figureSheetReader = new FigureSheetReader(GoogleService)
            {
                SheetID = SheetID,
                SheetName = FigureSheetName,
            };
            _figures = await figureSheetReader.ReadAsync(cancellationToken);
        }

        [ContextMenu("Print Figures")]
        public async void PrintFigures()
        {
            var figureSheetReader = new FigureSheetReader(GoogleService)
            {
                SheetID = SheetID,
                SheetName = FigureSheetName,
            };
            var figures = await figureSheetReader.ReadAsync();
            foreach (var figure in figures)
            {
                Debug.Log($"ID: {figure.id}, Name: {figure.name}, HP: {figure.hp}, description: {figure.description}");
            }
        }
    }
}