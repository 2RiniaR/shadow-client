using System;
using RineaR.Shadow.Network;
using UniRx;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using Zenject;

namespace RineaR.Shadow.UI
{
    [RequireComponent(typeof(PageContainer))]
    public class ScreenController : MonoBehaviour
    {
        private readonly ReactiveProperty<Phase> _currentPhase = new(Phase.None);

        private PageContainer _pageContainer;

        [Inject] private SessionConnector Connector { get; set; }

        private void Awake()
        {
            _pageContainer = GetComponent<PageContainer>();
            _currentPhase.Subscribe(OnPhaseChanged).AddTo(this);
        }

        private void Update()
        {
            if (!Connector.Client)
                _currentPhase.Value = Phase.Join;
            else
            {
                _currentPhase.Value = Connector.Client.Server.Phase switch
                {
                    SessionPhase.Matching => Phase.Matching,
                    SessionPhase.SelectingUnit => Phase.UnitSelect,
                    _ => _currentPhase.Value,
                };
            }
        }

        private void OnPhaseChanged(Phase phase)
        {
            switch (phase)
            {
                case Phase.None:
                    break;
                case Phase.Join:
                    _pageContainer.Push("Join Page", true, false,
                        onLoad: x => { x.page.GetComponent<JoinView>()?.Initialize(); });
                    break;
                case Phase.Matching:
                    _pageContainer.Push("Matching Page", true, false,
                        onLoad: x => { x.page.GetComponent<MatchingView>()?.Initialize(); });
                    break;
                case Phase.UnitSelect:
                    _pageContainer.Push("Unit Select Page", true, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(phase), phase, null);
            }
        }

        private enum Phase
        {
            None,
            Join,
            Matching,
            UnitSelect,
        }
    }
}