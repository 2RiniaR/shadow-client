using Fusion;
using RineaR.Shadow.Battles.Fields;
using UnityEngine;

namespace RineaR.Shadow.Battles.Gimmicks
{
    public class Gimmick : NetworkBehaviour
    {
        [Networked] public Vector2Int Position { get; set; }
        [Networked] public Direction Rotation { get; set; }
    }
}