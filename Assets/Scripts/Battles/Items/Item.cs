using Fusion;
using UnityEngine;

namespace RineaR.Shadow.Battles.Items
{
    public class Item : NetworkBehaviour
    {
        [Networked] public Vector2Int Position { get; set; }
    }
}