using RineaR.Shadow.Master;
using RineaR.Shadow.Network;
using UnityEngine;
using Zenject;

namespace RineaR.Shadow
{
    public class ClientInstaller : MonoInstaller
    {
        [SerializeField]
        private MasterSet masterSet;

        [SerializeField]
        private SessionConnector sessionConnector;

        public override void InstallBindings()
        {
            Container.Bind<IMasterRepository>().FromInstance(masterSet).AsSingle();
            Container.Bind<SessionConnector>().FromInstance(sessionConnector).AsSingle();
        }
    }
}