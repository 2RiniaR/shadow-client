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
using Zenject;

namespace RineaR.Shadow.UI
{
    public class MatchingView : MonoBehaviour
    {
        [SerializeField]
        private Button join;

        [SerializeField]
        private List<TMP_Text> players;

        [SerializeField]
        private Button submit;

        private CancellationTokenSource _cts;
        private TMP_Text _joinLabel;

        [Inject] private SessionConnector Connector { get; set; }

        public void OnDestroy()
        {
            _cts?.Cancel();
        }

        public void Initialize()
        {
            _cts = new CancellationTokenSource();

            join.OnClickAsObservable().Subscribe(_ => Connector.JoinSession().Forget()).AddTo(this);
            _joinLabel = join.GetComponentInChildren<TMP_Text>();

            submit.OnClickAsObservable().Subscribe(_ => Submit()).AddTo(this);

            LoopRefresh(_cts.Token).Forget();
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void Submit()
        {
            if (Connector.Client != null) Connector.Client.Server.SubmitPlayers();
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
            join.interactable = !Connector.Client;
            _joinLabel.text = Connector.Client ? "Joined" : "Join the room";

            if (Connector.Client)
            {
                for (var i = 0; i < Mathf.Max(4, players.Count); i++)
                {
                    players[i].text = $"Player {i + 1}: waiting...";
                }

                var own = Connector.Client;
                foreach (var client in own.Server.Clients.Where(client => client))
                {
                    players[client.Number].text = $"Player {client.Number + 1}: <JOINED>";
                }

                players[own.Number].text = $"Player {own.Number + 1}: <YOU>";
            }
            else
            {
                foreach (var playerLabel in players)
                {
                    playerLabel.text = "";
                }
            }

            // ゲームを開始可能になったら、開始ボタンを有効化する
            submit.gameObject.SetActive(Connector.Client != null &&
                                        Connector.Client.CanStartGame);
        }
    }
}