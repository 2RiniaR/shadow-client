using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace RineaR.Shadow.Master
{
    public interface IMasterRepository
    {
        UniTask Fetch();
        IEnumerable<FigureSetting> GetFigures();
        FigureSetting GetFigureByID(int id);
        IEnumerable<CardSetting> GetCards();
        CardSetting GetCardByID(int id);
        IEnumerable<FieldSetting> GetFields();
        FieldSetting GetFieldByID(int id);
    }
}