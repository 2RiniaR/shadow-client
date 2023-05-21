using RineaR.Shadow.Common.Phases;
using RineaR.Shadow.Rules;
using RineaR.Shadow.Views;
using UniRx;

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

            Scene.PageContainer.Push("Pages/Loading Page", true, false);
            await Scene.Master.FetchAsync();

            Scene.PageContainer.Push("Pages/Figure Select Page", true, false,
                onLoad: x =>
                {
                    if (_disposable == null || _disposable.IsDisposed) return;
                    var view = x.page.GetComponent<FigureSelectView>();
                    view.System = new FigureSelectSystem
                    {
                        Session = Scene.Session,
                        Master = Scene.Master,
                    };
                    view.System.Initialize();
                    view.Initialize();
                });
        }

        public void OnExitPhase()
        {
            _disposable?.Dispose();
            Scene.PageContainer.Pop(false);
        }
    }
}