using Fusion;
using RineaR.Shadow.Battles.Fields;
using UnityEngine;

namespace RineaR.Shadow.Battles.Figures
{
    /// <summary>
    ///     フィギュア。
    ///     プレイヤーが毎ターン指令を出して動かすユニット。
    /// </summary>
    public class Figure : NetworkBehaviour
    {
        /// <summary>
        ///     所属しているプレイヤー。
        /// </summary>
        [Networked]
        public BattlePlayer Owner { get; set; }

        /// <summary>
        ///     HP。
        /// </summary>
        [Networked]
        // ReSharper disable once InconsistentNaming
        public int HP { get; set; }

        /// <summary>
        ///     メド。
        /// </summary>
        [Networked]
        public int Medo { get; set; }

        /// <summary>
        ///     フィールドでの位置座標。
        /// </summary>
        [Networked]
        public Vector2Int Position { get; set; }

        /// <summary>
        ///     フィールドでの向き。
        /// </summary>
        [Networked]
        public Direction Face { get; set; }
    }
}