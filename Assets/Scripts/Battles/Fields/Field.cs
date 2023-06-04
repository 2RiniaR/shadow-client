using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fusion;
using UnityEngine;

namespace RineaR.Shadow.Battles.Fields
{
    public class Field : NetworkBehaviour
    {
        private readonly Dictionary<Vector2Int, Tile> _tiles = new();
        [Networked] public Battle Battle { get; set; }
        public ReadOnlyDictionary<Vector2Int, Tile> Tiles => new(_tiles);

        public override void Spawned()
        {
            if (Battle)
            {
                Battle.Field = this;
                transform.SetParent(Battle.transform);
            }
        }
    }
}