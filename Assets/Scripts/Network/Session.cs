using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RineaR.Shadow.Network
{
    public class Session : NetworkBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField]
        private string sessionName;

        [SerializeField]
        private SessionClient clientPrefab;

        [SerializeField]
        private NetworkRunner localRunner;

        private Dictionary<PlayerRef, SessionClient> _clients;

        [CanBeNull]
        private SessionClient _localClient;

        [CanBeNull]
        public SessionClient LocalClient
        {
            get
            {
                if (_localClient) return _localClient;
                if (!localRunner.IsRunning) return null;
                _localClient = localRunner.GetPlayerObject(localRunner.LocalPlayer)?.GetComponent<SessionClient>();
                return _localClient;
            }
        }

        /// <summary>
        ///     現在のフェーズ。
        /// </summary>
        [Networked]
        public SessionState State { get; set; } = SessionState.None;

        /// <summary>
        ///     バトルで使用するフィールドのID。
        /// </summary>
        [Networked]
        public NetworkString<_16> FieldID { get; set; }

        private void Awake()
        {
            _clients = new Dictionary<PlayerRef, SessionClient>();
            localRunner.AddCallbacks(this);
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (HasStateAuthority)
            {
                runner.Spawn(clientPrefab, inputAuthority: player, onBeforeSpawned: (runner, obj) =>
                {
                    var client = obj.GetComponent<SessionClient>();
                    client.Number = _clients.Count;
                    _clients.Add(player, client);
                    runner.SetPlayerObject(player, client.Object);
                });
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (HasStateAuthority)
            {
                if (!_clients.TryGetValue(player, out var leftClient)) return;

                foreach (var (_, client) in _clients)
                {
                    if (client.Number > leftClient.Number) client.Number--;
                }

                runner.Despawn(leftClient.Object);
                _clients.Remove(player);
            }
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
            runner.Shutdown(shutdownReason: ShutdownReason.Ok);
        }

        public override void Spawned()
        {
            State = SessionState.Matching;
        }

        public IEnumerable<SessionClient> GetAllClients()
        {
            var clients = new List<SessionClient>();
            foreach (var player in Runner.ActivePlayers)
            {
                var client = Runner.GetPlayerObject(player)?.GetComponent<SessionClient>();
                if (!client) continue;
                clients.Add(client);
            }

            return clients;
        }

        public async UniTask Join(CancellationToken cancellationToken = default)
        {
            await localRunner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.AutoHostOrClient,
                SessionName = sessionName != string.Empty ? sessionName : null,
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = localRunner.GetComponent<NetworkSceneManagerDefault>(),
            }).AsUniTask().AttachExternalCancellation(cancellationToken);
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void RPC_ConfirmPlayers()
        {
            if (State == SessionState.Matching) State = SessionState.FigureSelect;
        }

        public override void FixedUpdateNetwork()
        {
            if (State == SessionState.FigureSelect && GetAllClients().All(client => client.HasFigureConfirmed))
                State = SessionState.Battle;
        }

        #region 不要なコールバックメソッド

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            // do nothing
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
            // do nothing
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            // do nothing
        }

        // ReSharper disable once Unity.IncorrectMethodSignature
        public void OnConnectedToServer(NetworkRunner runner)
        {
            // do nothing
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
            byte[] token)
        {
            // do nothing
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            // do nothing
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
            // do nothing
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            // do nothing
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
            // do nothing
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
            // do nothing
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        {
            // do nothing
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
            // do nothing
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
            // do nothing
        }

        #endregion
    }
}