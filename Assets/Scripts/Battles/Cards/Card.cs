using Fusion;

namespace RineaR.Shadow.Battles.Cards
{
    public class Card : NetworkBehaviour
    {
        [Networked] public BattlePlayer Owner { get; set; }
    }
}