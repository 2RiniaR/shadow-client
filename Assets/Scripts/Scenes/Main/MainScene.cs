using RineaR.Shadow.Battles;
using RineaR.Shadow.Common.Phases;
using RineaR.Shadow.Master;
using RineaR.Shadow.Network;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using Zenject;

namespace RineaR.Shadow.Scenes.Main
{
    public class MainScene : MonoBehaviour
    {
        [field: SerializeField] public BattlePlayer PlayerPrefab { get; private set; }
        [field: SerializeField] public BattleAudience AudiencePrefab { get; private set; }
        [field: SerializeField] public Battle BattlePrefab { get; private set; }
        [field: SerializeField] public Session Session { get; private set; }
        [field: SerializeField] public PageContainer PageContainer { get; private set; }
        [Inject] public IMasterRepository Master { get; private set; }

        public PhaseSwitch Phases { get; private set; }
        public JoinPhase JoinPhase { get; private set; }
        public MatchingPhase MatchingPhase { get; private set; }
        public FigureSelectPhase FigureSelectPhase { get; private set; }
        public BattlePhase BattlePhase { get; private set; }

        private void Awake()
        {
            JoinPhase = new JoinPhase(this);
            MatchingPhase = new MatchingPhase(this);
            FigureSelectPhase = new FigureSelectPhase(this);
            BattlePhase = new BattlePhase(this);

            Phases = new PhaseSwitch();
        }

        private void Start()
        {
            Phases.Set(JoinPhase);
        }
    }
}