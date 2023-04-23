using Fusion;
using UnityEngine;

namespace RineaR.Shadow.Network
{
    /// <summary>
    ///     接続中のセッションに対する処理を行うクライアント。セッションへの接続中のみ存在する。
    /// </summary>
    public class SessionClient : NetworkBehaviour
    {
        [Networked] [Capacity(4)] public NetworkArray<int> UnitsID => default;
        [Networked] public SessionServer Server { get; set; }
        [Networked] public int Number { get; set; }
        public bool IsHost => Runner.IsServer;
        public bool CanStartGame => IsHost && Server.CanStartGame;

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_SubmitUnits(int[] unitsID)
        {
            UnitsID.CopyFrom(unitsID, 0, 4);
            Debug.Log($"Units: {string.Join(",", UnitsID)}");
        }
    }
}