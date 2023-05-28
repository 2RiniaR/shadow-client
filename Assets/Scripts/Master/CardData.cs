using System.Collections.Generic;
using RineaR.Shadow.Battles.Cards;
using UnityEngine;

namespace RineaR.Shadow.Master
{
    [CreateAssetMenu(menuName = Constants.CreateAssetMenuFolder + "/Card Data")]
    public class CardData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public CardType Type { get; set; }
        [field: SerializeField] public Sprite MainImage { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Explanation { get; set; }

        [field: SerializeField] public string OperationID { get; set; }
        [field: SerializeField] public List<string> OperationParameters { get; set; }
    }
}