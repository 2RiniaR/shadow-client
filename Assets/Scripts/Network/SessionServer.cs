using System.Linq;
using Fusion;

namespace RineaR.Shadow.Network
{
    public class SessionServer : NetworkBehaviour
    {
        public const int MinGamePlayers = 2;
        [Networked] public bool IsMatching { get; private set; }

        [Networked] [Capacity(20)] public NetworkArray<SessionClient> Clients => default;

        public bool CanStartGame => Runner.ActivePlayers.Count() >= MinGamePlayers;

        public void SubmitPlayers()
        {
            RPC_SubmitPlayers();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_StartMatching()
        {
            if (!Runner.IsServer) return;
            IsMatching = true;
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_SubmitPlayers()
        {
            if (!CanStartGame) return;
            IsMatching = false;
        }
    }
}