using System;
using UnityEngine;

namespace RineaR.Shadow.Master
{
    [Serializable]
    public class FigureSetting
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Description { get; set; }
        [field: SerializeField] public int Hp { get; set; }
        [field: SerializeField] public Sprite FaceImage { get; set; }
    }
}