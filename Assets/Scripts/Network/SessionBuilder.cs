using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace RineaR.Shadow.Network
{
    /// <summary>
    ///     アプリケーションがホストの時のみ稼働し、セッション内の環境構築を行う。
    /// </summary>
    [RequireComponent(typeof(NetworkRunner))]
    public class SessionBuilder : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField]
        private SessionServer serverPrefab;

        [SerializeField]
        private SessionClient clientPrefab;

        private readonly Dictionary<PlayerRef, SessionClient> _clients = new();
        private SessionServer _server;

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (!runner.IsServer) return;

            if (!_server) SpawnServer(runner);
            SpawnClient(runner, player);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            DespawnClient(runner, player);
        }

        private void DespawnClient(NetworkRunner runner, PlayerRef player)
        {
            if (!_clients.TryGetValue(player, out var leftClient)) return;

            foreach (var (_, client) in _clients)
            {
                if (client.Number > leftClient.Number) client.Number--;
            }

            runner.Despawn(leftClient.Object);
            _clients.Remove(player);
        }

        private void SpawnServer(NetworkRunner runner)
        {
            _server = runner.Spawn(serverPrefab);
        }

        private void SpawnClient(NetworkRunner runner, PlayerRef player)
        {
            var client = runner.Spawn(clientPrefab, inputAuthority: player);
            client.Number = _clients.Count;
            client.Server = _server;
            _server.Clients.Set(_clients.Count, client);
            _clients.Add(player, client);

            runner.SetPlayerObject(player, client.Object);
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

        #endregion
    }
}