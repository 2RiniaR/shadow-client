using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fusion;
using RineaR.Shadow.Battles.Fields;
using RineaR.Shadow.Battles.Phases;
using RineaR.Shadow.Common.Phases;
using UniRx;

namespace RineaR.Shadow.Battles
{
    public class Battle : NetworkBehaviour
    {
        /// <summary>
        ///     最小のプレイ可能人数。
        /// </summary>
        public const int MinGamePlayers = 2;

        private readonly List<BattleAudience> _audiences = new();
        private readonly List<BattlePlayer> _players = new();

        /// <summary>
        ///     参加している観戦者。
        /// </summary>
        public ReadOnlyCollection<BattleAudience> Audiences => _audiences.AsReadOnly();

        /// <summary>
        ///     参加しているプレイヤー。
        /// </summary>
        public ReadOnlyCollection<BattlePlayer> Players => _players.AsReadOnly();

        /// <summary>
        ///     使用しているフィールド。
        /// </summary>
        public Field Field { get; private set; }

        /// <summary>
        ///     現在のターン。
        /// </summary>
        [Networked]
        public int Turn { get; set; } = 1;

        public PhaseSwitch<BattlePhaseName> PhaseSwitch { get; } = new(BattlePhaseName.NotStarted);

        public ReadyPhase ReadyPhase { get; } = new();
        public OperatePhase OperatePhase { get; } = new();
        public ActPhase ActPhase { get; } = new();
        public GameSetPhase GameSetPhase { get; } = new();

        public void Initialize()
        {
            PhaseSwitch.RegisterHandler(BattlePhaseName.Ready, ReadyPhase);
            PhaseSwitch.RegisterHandler(BattlePhaseName.Operate, OperatePhase);
            PhaseSwitch.RegisterHandler(BattlePhaseName.Act, ActPhase);
            PhaseSwitch.RegisterHandler(BattlePhaseName.GameSet, GameSetPhase);
            PhaseSwitch.Initialize();
            PhaseSwitch.AddTo(this);

            PhaseSwitch.Set(BattlePhaseName.Ready);
        }

        public void Join(BattlePlayer player)
        {
            _players.Add(player);
            player.transform.SetParent(transform);
        }

        public void Join(BattleAudience audience)
        {
            _audiences.Add(audience);
            audience.transform.SetParent(transform);
        }

        public void Use(Field field)
        {
            Field = field;
            field.transform.SetParent(transform);
        }
    }
}