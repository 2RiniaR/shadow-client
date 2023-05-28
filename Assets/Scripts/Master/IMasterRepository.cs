using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace RineaR.Shadow.Master
{
    public interface IMasterRepository
    {
        UniTask FetchAsync(CancellationToken cancellationToken = default);
        IEnumerable<FigureData> GetFigures();
        FigureData GetFigureByID(string id);
        IEnumerable<CardData> GetCards();
        CardData GetCardByID(string id);
        IEnumerable<FieldData> GetFields();
        FieldData GetFieldByID(string id);
        IEnumerable<PassiveData> GetPassives();
        PassiveData GetPassiveByID(string id);
    }
}