using Cysharp.Threading.Tasks;
using RineaR.Shadow.Network;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RineaR.Shadow.Views
{
    public class JoinView : MonoBehaviour
    {
        [SerializeField]
        private Button join;

        [Inject] private SessionConnector Connector { get; set; }

        public void Initialize()
        {
            join.OnClickAsObservable().Subscribe(_ => Connector.JoinSession().Forget()).AddTo(this);
        }
    }
}