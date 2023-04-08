using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using RineaR.MadeHighlow.Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace RineaR.MadeHighlow.Common
{
    public class RemoteManager : MonoBehaviour, INetworkRunnerCallbacks
    {
        private const string DefaultRoomName = "Default";

        [SerializeField]
        private Client clientPrefab;

        private readonly Dictionary<PlayerRef, Client> _clients = new();

        [Inject] private NetworkRunner NetworkRunner { get; set; }

        private void OnDestroy()
        {
            NetworkRunner.RemoveCallbacks(this);
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (!runner.IsServer) return;

            var client = runner.Spawn(clientPrefab, inputAuthority: player);
            client.ClientNumber = _clients.Count;
            _clients.Add(player, client);

            runner.SetPlayerObject(player, client.Object);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (!_clients.TryGetValue(player, out var leftClient)) return;

            foreach (var (_, client) in _clients)
            {
                if (client.ClientNumber > leftClient.ClientNumber) client.ClientNumber--;
            }

            runner.Despawn(leftClient.Object);
            _clients.Remove(player);
        }

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

        public void OnConnectedToServer(NetworkRunner runner)
        {
            // do nothing
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
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

        public void Initialize()
        {
            NetworkRunner.AddCallbacks(this);
        }

        public async UniTask<StartGameResult> Join()
        {
            return await NetworkRunner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.AutoHostOrClient,
                SessionName = DefaultRoomName,
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = NetworkRunner.GetComponent<NetworkSceneManagerDefault>(),
            });
        }
    }
}