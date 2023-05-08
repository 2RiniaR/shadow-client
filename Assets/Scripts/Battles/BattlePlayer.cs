﻿using System.Collections.Generic;
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
        public List<Figure> Figures { get; } = new();
        public List<Card> Cards { get; } = new();
    }
}