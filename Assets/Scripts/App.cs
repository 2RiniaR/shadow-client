using RineaR.Shadow.Common.Phases;
using RineaR.Shadow.Network;
using RineaR.Shadow.Phases;
using UniRx;
using UnityEngine;
using Zenject;

namespace RineaR.Shadow
{
    public class App : MonoBehaviour
    {
        public PhaseSwitch<AppPhaseName> PhaseSwitch { get; } = new(AppPhaseName.Join);

        [Inject] private SessionConnector Connector { get; set; }

        public JoinPhase JoinPhase { get; } = new();
        public MatchingPhase MatchingPhase { get; } = new();
        public FigureSelectPhase FigureSelectPhase { get; } = new();
        public BattlePhase BattlePhase { get; } = new();

        [Inject] private DiContainer DiContainer { get; set; }

        private void Awake()
        {
            DiContainer.Inject(JoinPhase);
            DiContainer.Inject(MatchingPhase);
            DiContainer.Inject(FigureSelectPhase);
            DiContainer.Inject(BattlePhase);

            PhaseSwitch.RegisterHandler(AppPhaseName.Join, JoinPhase);
            PhaseSwitch.RegisterHandler(AppPhaseName.Matching, MatchingPhase);
            PhaseSwitch.RegisterHandler(AppPhaseName.FigureSelect, FigureSelectPhase);
            PhaseSwitch.RegisterHandler(AppPhaseName.Battle, BattlePhase);
            PhaseSwitch.Initialize();
            PhaseSwitch.AddTo(this);
        }

        private void Update()
        {
            if (!Connector.Client)
                PhaseSwitch.Set(AppPhaseName.Join);
            else
            {
                var phaseName = Connector.Client.Server.PhaseName switch
                {
                    SessionPhaseName.Matching => AppPhaseName.Matching,
                    SessionPhaseName.FigureSelect => AppPhaseName.FigureSelect,
                    _ => AppPhaseName.None,
                };
                PhaseSwitch.Set(phaseName);
            }
        }
    }
}