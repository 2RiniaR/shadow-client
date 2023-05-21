using Fusion;
using RineaR.Shadow.Battles;

namespace RineaR.Shadow.Network
{
    public class SessionClient : NetworkBehaviour
    {
        /// <summary>
        ///     対戦のプレイヤー。プレイヤーとして対戦に参加している時のみ有効。
        /// </summary>
        [Networked]
        public BattlePlayer Player { get; set; }

        /// <summary>
        ///     対戦の観戦者。観戦者として対戦に参加している時のみ有効。
        /// </summary>
        [Networked]
        public BattleAudience Audience { get; set; }

        /// <summary>
        ///     整理番号。0から順番に入室順に付与される。前の番号のクライアントが退出すると、番号がその分だけ前に移動する。
        /// </summary>
        [Networked]
        public int Number { get; set; }

        /// <summary>
        ///     プレイヤーとしてバトルに参加するかどうか。
        /// </summary>
        [Networked]
        public SessionClientRole Role { get; set; }

        /// <summary>
        ///     バトルで使用するフィギュアのID。
        /// </summary>
        [Networked]
        [Capacity(4)]
        public NetworkArray<NetworkString<_16>> FiguresID => default;

        [Networked] public bool HasFigureConfirmed { get; set; }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_ConfirmFigures(string[] figuresID)
        {
            for (var i = 0; i < figuresID.Length; i++)
            {
                FiguresID.Set(i, figuresID[i]);
            }

            HasFigureConfirmed = true;
        }
    }
}