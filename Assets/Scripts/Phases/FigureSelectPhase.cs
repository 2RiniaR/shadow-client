using RineaR.Shadow.Common.Phases;
using RineaR.Shadow.Views;
using UnityScreenNavigator.Runtime.Core.Page;
using Zenject;

namespace RineaR.Shadow.Phases
{
    public class FigureSelectPhase : IPhaseHandler
    {
        [Inject] public PageContainer PageContainer { get; private set; }

        public void Start()
        {
            PageContainer.Push("Figure Select Page", true, false,
                onLoad: x => { x.page.GetComponent<FigureSelectView>()?.Initialize(); });
        }

        public void Finish()
        {
            PageContainer.Pop(false);
        }
    }
}