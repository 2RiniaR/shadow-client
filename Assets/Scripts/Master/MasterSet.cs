using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace RineaR.Shadow.Master
{
    [CreateAssetMenu(menuName = "shadow/MasterSet")]
    public class MasterSet : ScriptableObject, IMasterRepository
    {
        public List<FigureSetting> figures;
        public List<CardSetting> cards;
        public List<FieldSetting> fields;

        public IEnumerable<FigureSetting> GetFigures()
        {
            return figures;
        }

        [CanBeNull]
        public FigureSetting GetFigureByID(int id)
        {
            return figures.Find(figure => figure.id == id);
        }

        [CanBeNull]
        public CardSetting GetCardByID(int id)
        {
            return cards.Find(card => card.id == id);
        }

        public FieldSetting GetFieldByID(int id)
        {
            return fields.Find(field => field.id == id);
        }

        public UniTask Fetch()
        {
            // do nothing
            return UniTask.CompletedTask;
        }

        public IEnumerable<CardSetting> GetCards()
        {
            return cards;
        }

        public IEnumerable<FieldSetting> GetFields()
        {
            return fields;
        }
    }
}