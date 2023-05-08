using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fusion;
using UnityEngine;

namespace RineaR.Shadow.Battles.Fields
{
    public class Field : NetworkBehaviour
    {
        private readonly Dictionary<Vector2Int, Tile> _tiles = new();
        public ReadOnlyDictionary<Vector2Int, Tile> Tiles => new(_tiles);
    }
}