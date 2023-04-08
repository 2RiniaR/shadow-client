using Fusion;
using RineaR.MadeHighlow.Network;
using UnityEngine;
using Zenject;

namespace RineaR.MadeHighlow.Common
{
    public class ClientInstaller : MonoInstaller
    {
        [SerializeField]
        private NetworkRunner networkRunner;

        [SerializeField]
        private RemoteManager remoteManager;

        [SerializeField]
        private MatchingWindow matchingWindow;

        public override void InstallBindings()
        {
            Container.Bind<NetworkRunner>().FromInstance(networkRunner).AsSingle();
            Container.Bind<RemoteManager>().FromInstance(remoteManager).AsSingle();
            Container.Bind<MatchingWindow>().FromInstance(matchingWindow).AsSingle();
        }
    }
}