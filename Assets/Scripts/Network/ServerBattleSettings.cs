using Fusion;

namespace RineaR.Shadow.Network
{
    public struct ServerBattleSettings : INetworkStruct
    {
        /// <summary>
        ///     バトルで使用するフィールドのID。
        /// </summary>
        [Networked]
        public string FieldID { get; set; }
    }
}