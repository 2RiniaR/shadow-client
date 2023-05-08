using System.Linq;
using Fusion;
using RineaR.Shadow.Battles;
using RineaR.Shadow.Master;
using Zenject;

namespace RineaR.Shadow.Network
{
    /// <summary>
    ///     セッションのサーバー。
    ///     セッション中に1つしか存在せず、セッションへの接続中のみ存在する。
    /// </summary>
    public class SessionServer : NetworkBehaviour
    {
        /// <summary>
        ///     現在のフェーズ。
        /// </summary>
        [Networked]
        public SessionPhaseName PhaseName { get; set; } = SessionPhaseName.Initial;

        /// <summary>
        ///     クライアントへの参照。
        /// </summary>
        [Networked]
        [Capacity(20)]
        public NetworkArray<SessionClient> Clients => default;

        /// <summary>
        ///     現在行われている対戦。
        /// </summary>
        [Networked]
        public Battle Battle { get; set; }

        /// <summary>
        ///     対戦用の設定。
        /// </summary>
        [Networked]
        public ServerBattleSettings BattleSettings { get; set; }

        [Inject] public IMasterRepository Master { get; }
        [Inject] public AppSettings AppSettings { get; }

        public bool CanStartGame()
        {
            return Runner.ActivePlayers.Count() >= Battle.MinGamePlayers;
        }
    }
}