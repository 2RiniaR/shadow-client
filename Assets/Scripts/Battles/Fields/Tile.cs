using Fusion;
using UnityEngine;

namespace RineaR.Shadow.Battles.Fields
{
    public class Tile : NetworkBehaviour
    {
        [Networked] public Vector2Int Position { get; set; }
        [Networked] public Direction Rotation { get; set; }
    }
}