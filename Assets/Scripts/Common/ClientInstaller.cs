using RineaR.Shadow.Master;
using RineaR.Shadow.Network;
using RineaR.Shadow.UI;
using UnityEngine;
using Zenject;

namespace RineaR.Shadow.Common
{
    public class ClientInstaller : MonoInstaller
    {
        [SerializeField]
        private MasterSet masterSet;

        [SerializeField]
        private MatchingView matchingView;

        [SerializeField]
        private UnitSelectView unitSelectView;

        [SerializeField]
        private SessionConnector sessionConnector;

        public override void InstallBindings()
        {
            Container.Bind<IMasterRepository>().FromInstance(masterSet).AsSingle();
            Container.Bind<SessionConnector>().FromInstance(sessionConnector).AsSingle();
            Container.Bind<MatchingView>().FromInstance(matchingView).AsSingle();
            Container.Bind<UnitSelectView>().FromInstance(unitSelectView).AsSingle();
        }
    }
}