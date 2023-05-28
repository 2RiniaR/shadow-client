using System.Linq;
using Cysharp.Threading.Tasks;
using RineaR.Shadow.Common.Phases;
using RineaR.Shadow.Network;
using RineaR.Shadow.Rules;
using RineaR.Shadow.Views;
using UniRx;
using UnityEngine;

namespace RineaR.Shadow.Scenes.Main
{
    public class FigureSelectPhase : IPhase
    {
        private CompositeDisposable _disposable;

        public FigureSelectPhase(MainScene scene)
        {
            Scene = scene;
        }

        public MainScene Scene { get; }

        public async void OnEnterPhase()
        {
            _disposable = new CompositeDisposable();
            await UniTask.WaitWhile(() => Scene.PageContainer.IsInTransition);

            Scene.PageContainer.Push("Pages/Loading Page", true, false);
            await Scene.Master.FetchAsync();

            await UniTask.WaitWhile(() => Scene.PageContainer.IsInTransition);
            Scene.PageContainer.Push("Pages/Figure Select Page", true, false,
                onLoad: x =>
                {
                    if (_disposable == null || _disposable.IsDisposed) return;

                    var system = new FigureSelectSystem { Session = Scene.Session, Master = Scene.Master };
                    system.Initialize();
                    system.OnConfirmed.Subscribe(_ =>
                    {
                        if (!Scene.Session.LocalClient)
                        {
                            Debug.LogWarning("オンラインに接続していません。");
                            return;
                        }

                        Scene.Session.LocalClient
                            .RPC_ConfirmFigures(system.Selections.Select(figure => figure!.name).ToArray());
                    }).AddTo(_disposable);

                    var view = x.page.GetComponent<FigureSelectView>();
                    view.System = system;
                    view.Initialize();
                });

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (Scene.Session.Runner && Scene.Session.State == SessionState.Battle)
                    Scene.Phases.Set(Scene.BattlePhase);
            }).AddTo(_disposable);
        }

        public void OnExitPhase()
        {
            _disposable?.Dispose();
            Scene.PageContainer.Pop(false);
        }
    }
}