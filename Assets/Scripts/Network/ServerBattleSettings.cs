using Fusion;

namespace RineaR.Shadow.Network
{
    public struct ServerBattleSettings : INetworkStruct
    {
        /// <summary>
        ///     バトルで使用するフィールドのID。
        /// </summary>
        [Networked]
        public int FieldID { get; set; }
    }
}