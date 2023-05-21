using Fusion;

namespace RineaR.Shadow.Network
{
    public struct BattleClientSettings : INetworkStruct
    {
        /// <summary>
        ///     プレイヤーとしてバトルに参加するかどうか。
        /// </summary>
        [Networked]
        public BattleClientRole Role { get; set; }

        /// <summary>
        ///     バトルで使用するフィギュアのID。
        /// </summary>
        [Networked]
        [Capacity(4)]
        public NetworkArray<NetworkString<_16>> FiguresID => default;

        public void SetFigures(string[] figuresID)
        {
            for (var i = 0; i < figuresID.Length; i++)
            {
                FiguresID.Set(i, figuresID[i]);
            }
        }
    }
}