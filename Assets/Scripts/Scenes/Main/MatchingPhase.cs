using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.Shadow.Common.Phases;
using RineaR.Shadow.Network;
using RineaR.Shadow.Views;
using UniRx;

namespace RineaR.Shadow.Scenes.Main
{
    public class MatchingPhase : IPhase
    {
        private CancellationTokenSource _cts;
        private CompositeDisposable _disposable;

        public MatchingPhase(MainScene scene)
        {
            Scene = scene;
        }

        public MainScene Scene { get; }

        public void OnEnterPhase()
        {
            _disposable = new CompositeDisposable();

            _cts = new CancellationTokenSource();
            _cts.AddTo(_disposable);

            Scene.PageContainer.Push("Pages/Matching Page", true, false,
                onLoad: x =>
                {
                    if (_disposable == null || _disposable.IsDisposed) return;
                    var view = x.page.GetComponent<MatchingView>();
                    view.Session = Scene.Session;
                    view.Initialize();
                    view.OnMemberConfirmed
                        .Subscribe(_ =>
                        {
                            if (Scene.Session.HasStateAuthority) Scene.Session.State = SessionState.FigureSelect;
                        })
                        .AddTo(_disposable);
                });

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (Scene.Session.Runner && Scene.Session.State == SessionState.FigureSelect)
                    Scene.Phases.Set(Scene.FigureSelectPhase);
            }).AddTo(_disposable);

            Scene.Session.Join(_cts.Token).Forget();
        }

        public void OnExitPhase()
        {
            _disposable?.Dispose();
            Scene.PageContainer.Pop(false);
        }
    }
}