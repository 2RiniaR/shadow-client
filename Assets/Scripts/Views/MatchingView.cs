using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.Shadow.Network;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RineaR.Shadow.Views
{
    public class MatchingView : MonoBehaviour
    {
        [SerializeField]
        private List<TMP_Text> players;

        [SerializeField]
        private Button submit;

        public Session Session { get; set; }

        public IObservable<Unit> OnMemberConfirmed
        {
            get
            {
                if (submit) return submit.OnClickAsObservable().First().AsUnitObservable();
                return Observable.Never<Unit>();
            }
        }

        public void Initialize()
        {
            LoopRefresh(destroyCancellationToken).Forget();
        }

        private async UniTask LoopRefresh(CancellationToken token = default)
        {
            while (true)
            {
                Refresh();
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            }
        }

        public void Refresh()
        {
            if (Session.LocalClient)
            {
                for (var i = 0; i < Mathf.Max(4, players.Count); i++)
                {
                    players[i].text = $"Player {i + 1}: waiting...";
                }

                foreach (var client in Session.GetAllClients())
                {
                    players[client.Number].text = $"Player {client.Number + 1}: <JOINED>";
                }

                players[Session.LocalClient.Number].text = $"Player {Session.LocalClient.Number + 1}: <YOU>";
            }
            else
            {
                for (var i = 0; i < Mathf.Max(4, players.Count); i++)
                {
                    players[i].text = $"Player {i + 1}: ---";
                }
            }

            // ゲームを開始可能になったら、開始ボタンを有効化する
            submit.gameObject.SetActive(Session.LocalClient &&
                                        Session.HasStateAuthority &&
                                        Session.Runner.ActivePlayers.Count() >= 2);
        }
    }
}