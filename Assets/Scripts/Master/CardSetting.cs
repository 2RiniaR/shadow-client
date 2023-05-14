using System;
using UnityEngine;

namespace RineaR.Shadow.Master
{
    [Serializable]
    public class CardSetting
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public string Name { get; set; }
    }
}