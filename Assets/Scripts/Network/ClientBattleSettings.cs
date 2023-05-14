﻿using Fusion;

namespace RineaR.Shadow.Network
{
    public struct ClientBattleSettings : INetworkStruct
    {
        /// <summary>
        ///     プレイヤーとしてバトルに参加するかどうか。
        ///     trueの場合はプレイヤー、falseの場合は観戦者として参加する。
        /// </summary>
        [Networked]
        public bool JoinAsPlayer { get; set; }

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