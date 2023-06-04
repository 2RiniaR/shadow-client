using System.Collections.Generic;
using Fusion;
using RineaR.Shadow.Battles.Fields;
using RineaR.Shadow.Battles.Phases;
using RineaR.Shadow.Common.Phases;

namespace RineaR.Shadow.Battles
{
    public class Battle : NetworkBehaviour
    {
        /// <summary>
        ///     最小のプレイ可能人数。
        /// </summary>
        public const int MinGamePlayers = 2;

        /// <summary>
        ///     最大のプレイ可能人数。
        /// </summary>
        public const int MaxGamePlayers = 4;

        /// <summary>
        ///     参加している観戦者。
        /// </summary>
        public List<BattleAudience> Audiences { get; } = new();

        /// <summary>
        ///     参加しているプレイヤー。
        /// </summary>
        public List<BattlePlayer> Players { get; } = new();

        /// <summary>
        ///     使用しているフィールド。
        /// </summary>
        public Field Field { get; set; }

        /// <summary>
        ///     現在のターン。
        /// </summary>
        [Networked]
        public int Turn { get; set; } = 1;

        public PhaseSwitch Phases { get; private set; }

        public ReadyPhase ReadyPhase { get; } = new();
        public OperatePhase OperatePhase { get; } = new();
        public ActPhase ActPhase { get; } = new();
        public GameSetPhase GameSetPhase { get; } = new();

        public void Initialize()
        {
            Phases = new PhaseSwitch();
            Phases.Set(ReadyPhase);
        }
    }
}