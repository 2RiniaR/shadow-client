using RineaR.Shadow.Master;
using RineaR.Shadow.Master.Spreadsheet;
using UnityEngine;
using Zenject;

namespace RineaR.Shadow.Scenes.Main
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private MasterSpreadsheet masterSpreadsheet;

        public override void InstallBindings()
        {
            Container.Bind<IMasterRepository>().FromInstance(masterSpreadsheet).AsSingle();
        }
    }
}