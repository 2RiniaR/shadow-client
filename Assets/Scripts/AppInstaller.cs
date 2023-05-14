using RineaR.Shadow.Master;
using RineaR.Shadow.Master.Spreadsheet;
using RineaR.Shadow.Network;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using Zenject;

namespace RineaR.Shadow
{
    public class AppInstaller : MonoInstaller
    {
        [SerializeField]
        private MasterSpreadsheet masterSpreadsheet;

        [SerializeField]
        private AppSettings appSettings;

        [SerializeField]
        private SessionConnector sessionConnector;

        [SerializeField]
        private PageContainer pageContainer;

        public override void InstallBindings()
        {
            Container.Bind<IMasterRepository>().FromInstance(masterSpreadsheet).AsSingle();
            Container.Bind<AppSettings>().FromInstance(appSettings).AsSingle();
            Container.Bind<SessionConnector>().FromInstance(sessionConnector).AsSingle();
            Container.Bind<PageContainer>().FromInstance(pageContainer).AsSingle();
        }
    }
}