using System;
using RineaR.Shadow.Network;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RineaR.Shadow.Views
{
    public class JoinSessionView : MonoBehaviour
    {
        [SerializeField]
        private Button join;

        public Session Session { get; set; }

        public IObservable<Unit> OnJoinSelected
        {
            get
            {
                if (join) return join.OnClickAsObservable().First().AsUnitObservable();
                return Observable.Never<Unit>();
            }
        }
    }
}