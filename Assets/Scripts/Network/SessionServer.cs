using System.Linq;
using Fusion;

namespace RineaR.Shadow.Network
{
    public class SessionServer : NetworkBehaviour
    {
        public const int MinGamePlayers = 2;

        [Networked] public SessionPhase Phase { get; set; }

        [Networked] [Capacity(20)] public NetworkArray<SessionClient> Clients => default;

        public bool CanStartGame => Runner.ActivePlayers.Count() >= MinGamePlayers;

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_StartMatching()
        {
            if (!Runner.IsServer) return;
            Phase = SessionPhase.Matching;
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_SubmitPlayers()
        {
            if (!CanStartGame) return;
            Phase = SessionPhase.SelectingUnit;
        }
    }
}