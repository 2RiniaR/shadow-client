using Fusion;

namespace RineaR.Shadow.Network
{
    public struct BattleSettings : INetworkStruct
    {
        /// <summary>
        ///     バトルで使用するフィールドのID。
        /// </summary>
        public NetworkString<_16> FieldID { get; set; }
    }
}