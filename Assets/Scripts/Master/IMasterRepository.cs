using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.Shadow.Master
{
    public interface IMasterRepository
    {
        UniTask FetchAsync(CancellationToken cancellationToken = default);
        IEnumerable<FigureSetting> GetFigures();
        FigureSetting GetFigureByID(string id);
        IEnumerable<CardSetting> GetCards();
        CardSetting GetCardByID(string id);
        IEnumerable<FieldSetting> GetFields();
        FieldSetting GetFieldByID(string id);
    }
}