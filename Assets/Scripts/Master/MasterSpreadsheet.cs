using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.Shadow.Spreadsheet;
using Zenject;

namespace RineaR.Shadow.Master
{
    public class MasterSpreadsheet : IMasterRepository
    {
        private const string SHEET_ID = "142y26hQl-4xAU_daJk6lAtCp0Os8FEfGN_s-qy-lhv8";
        private List<CardSetting> _cards;
        private List<FieldSetting> _fields;
        private List<FigureSetting> _figures;

        [Inject] public GoogleService GoogleService { get; set; }

        public IEnumerable<FigureSetting> GetFigures()
        {
            return _figures.AsReadOnly();
        }

        public FigureSetting GetFigureByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CardSetting> GetCards()
        {
            return _cards.AsReadOnly();
        }

        public CardSetting GetCardByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FieldSetting> GetFields()
        {
            return _fields.AsReadOnly();
        }

        public FieldSetting GetFieldByID(int id)
        {
            throw new NotImplementedException();
        }

        public async UniTask FetchAsync(CancellationToken cancellationToken = default)
        {
            var figureReader = new FigureSheetReader(GoogleService)
            {
                SheetID = SHEET_ID,
                SheetName = "Figures",
            };
            _figures = (await figureReader.ReadAsync(cancellationToken)).ToList();
        }
    }
}