using System;
using RineaR.Shadow.Battles.Fields;
using UnityEngine;

namespace RineaR.Shadow.Master
{
    [Serializable]
    public class FieldSetting
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public Field Field { get; set; }
    }
}