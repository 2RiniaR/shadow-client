using RineaR.Shadow.Common.Phases;
using RineaR.Shadow.Views;
using UniRx;

namespace RineaR.Shadow.Scenes.Main
{
    public class JoinPhase : IPhase
    {
        private CompositeDisposable _disposable;

        public JoinPhase(MainScene scene)
        {
            Scene = scene;
        }

        public MainScene Scene { get; }

        public void OnEnterPhase()
        {
            _disposable = new CompositeDisposable();

            Scene.PageContainer.Push("Pages/Join Page", true, false,
                onLoad: x =>
                {
                    if (_disposable == null || _disposable.IsDisposed) return;
                    var view = x.page.GetComponent<JoinSessionView>();
                    view.Session = Scene.Session;
                    view.OnJoinSelected
                        .Subscribe(_ => Scene.Phases.Set(Scene.MatchingPhase))
                        .AddTo(_disposable);
                });
        }

        public void OnExitPhase()
        {
            _disposable?.Dispose();
            Scene.PageContainer.Pop(false);
        }
    }
}