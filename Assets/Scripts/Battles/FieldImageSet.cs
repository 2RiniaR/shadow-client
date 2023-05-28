using System;
using UnityEngine;

namespace RineaR.Shadow.Battles
{
    [Serializable]
    public class FieldImageSet
    {
        [field: SerializeField] public Sprite Up { get; set; }
        [field: SerializeField] public Sprite Down { get; set; }
        [field: SerializeField] public Sprite Left { get; set; }
        [field: SerializeField] public Sprite Right { get; set; }
    }
}