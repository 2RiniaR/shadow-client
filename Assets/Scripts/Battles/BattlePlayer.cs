using System.Collections.Generic;
using Fusion;
using RineaR.Shadow.Battles.Cards;
using RineaR.Shadow.Battles.Figures;

namespace RineaR.Shadow.Battles
{
    /// <summary>
    ///     試合のプレイヤー。
    /// </summary>
    public class BattlePlayer : NetworkBehaviour
    {
        [Networked] public Battle Battle { get; set; }

        public List<Figure> Figures { get; } = new();
        public List<Card> Cards { get; } = new();

        public override void Spawned()
        {
            if (Battle)
            {
                Battle.Players.Add(this);
                transform.SetParent(Battle.transform);
            }
        }
    }
}