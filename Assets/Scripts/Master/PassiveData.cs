using System.Collections.Generic;
using UnityEngine;

namespace RineaR.Shadow.Master
{
    [CreateAssetMenu(menuName = Constants.CreateAssetMenuFolder + "/Passive Data")]
    public class PassiveData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Explanation { get; set; }

        [field: SerializeField] public string EffectID { get; set; }
        [field: SerializeField] public List<string> EffectParameters { get; set; }
    }
}