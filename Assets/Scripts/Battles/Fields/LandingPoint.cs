using Fusion;
using UnityEngine;

namespace RineaR.Shadow.Battles.Fields
{
    public class LandingPoint : NetworkBehaviour
    {
        [Networked] public Vector2Int Position { get; set; }
    }
}