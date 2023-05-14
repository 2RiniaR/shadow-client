using System.Collections.Generic;
using System.Threading;
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

        public UniTask FetchAsync(CancellationToken cancellationToken = default)
        {
            // do nothing
            return UniTask.CompletedTask;
        }

        public IEnumerable<FigureSetting> GetFigures()
        {
            return figures;
        }

        [CanBeNull]
        public FigureSetting GetFigureByID(string id)
        {
            return figures.Find(figure => figure.ID == id);
        }

        [CanBeNull]
        public CardSetting GetCardByID(string id)
        {
            return cards.Find(card => card.ID == id);
        }

        public FieldSetting GetFieldByID(string id)
        {
            return fields.Find(field => field.ID == id);
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