using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.Shadow.Master
{
    public interface IMasterRepository
    {
        UniTask FetchAsync(CancellationToken cancellationToken = default);
        IEnumerable<FigureSetting> GetFigures();
        FigureSetting GetFigureByID(int id);
        IEnumerable<CardSetting> GetCards();
        CardSetting GetCardByID(int id);
        IEnumerable<FieldSetting> GetFields();
        FieldSetting GetFieldByID(int id);
    }
}