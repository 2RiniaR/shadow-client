using RineaR.Shadow.Master;
using Zenject;

namespace RineaR.Shadow.Scenes.Main
{
    public class MainSceneInstaller : MonoInstaller
    {
        public AddressableMaster master;

        public override void InstallBindings()
        {
            Container.Bind<IMasterRepository>().FromInstance(master).AsSingle();
        }
    }
}