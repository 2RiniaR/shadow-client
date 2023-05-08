using RineaR.Shadow.Master;
using RineaR.Shadow.Network;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using Zenject;

namespace RineaR.Shadow
{
    public class AppInstaller : MonoInstaller
    {
        [SerializeField]
        private MasterSet masterSet;

        [SerializeField]
        private AppSettings appSettings;

        [SerializeField]
        private SessionConnector sessionConnector;

        [SerializeField]
        private PageContainer pageContainer;

        public override void InstallBindings()
        {
            Container.Bind<IMasterRepository>().FromInstance(masterSet).AsSingle();
            Container.Bind<AppSettings>().FromInstance(appSettings).AsSingle();
            Container.Bind<SessionConnector>().FromInstance(sessionConnector).AsSingle();
            Container.Bind<PageContainer>().FromInstance(pageContainer).AsSingle();
        }
    }
}