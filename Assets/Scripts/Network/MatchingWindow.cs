using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;
using RineaR.MadeHighlow.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RineaR.MadeHighlow.Network
{
    public class MatchingWindow : MonoBehaviour
    {
        [SerializeField]
        private Button joinButton;

        [SerializeField]
        private TMP_Text joinButtonLabel;

        [SerializeField]
        private List<TMP_Text> playerLabels;

        [SerializeField]
        private Button submitButton;

        private CancellationTokenSource _cts;

        [Inject] private RemoteManager RemoteManager { get; set; }

        [Inject] private NetworkRunner NetworkRunner { get; set; }

        private void OnEnable()
        {
            _cts = new CancellationTokenSource();
            LoopRefresh(_cts.Token).Forget();
        }

        private void OnDisable()
        {
            _cts?.Cancel();
        }

        public void Initialize()
        {
            joinButton.onClick.AddListener(() => RemoteManager.Join().Forget());
            submitButton.onClick.AddListener(Submit);

            Refresh();
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
            joinButton.interactable = !NetworkRunner.IsRunning;
            joinButtonLabel.text = NetworkRunner.IsRunning ? "Joined" : "Join the room";

            if (NetworkRunner.IsRunning)
            {
                for (var i = 0; i < Mathf.Max(4, playerLabels.Count); i++)
                {
                    playerLabels[i].text = $"Player {i + 1}: waiting...";
                }

                var selfPlayerObject = NetworkRunner.GetPlayerObject(NetworkRunner.LocalPlayer);
                if (selfPlayerObject)
                {
                    var selfClient = selfPlayerObject.GetComponent<Client>();
                    playerLabels[selfClient.ClientNumber].text = $"Player {selfClient.ClientNumber + 1}: <YOU>";
                }

                foreach (var player in NetworkRunner.ActivePlayers)
                {
                    var playerObject = NetworkRunner.GetPlayerObject(player);
                    if (playerObject && playerObject != selfPlayerObject)
                    {
                        var client = playerObject.GetComponent<Client>();
                        playerLabels[client.ClientNumber].text = $"Player {client.ClientNumber + 1}: <JOINED>";
                    }
                }
            }
            else
            {
                foreach (var playerLabel in playerLabels)
                {
                    playerLabel.text = "";
                }
            }

            // ゲームを開始可能になったら、開始ボタンを有効化する
            submitButton.gameObject.SetActive(NetworkRunner.IsServer &&
                                              NetworkRunner.ActivePlayers.Count() >= 2);
        }
    }
}