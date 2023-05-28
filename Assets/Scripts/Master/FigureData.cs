using System;
using System.Collections.Generic;
using RineaR.Shadow.Battles;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace RineaR.Shadow.Master
{
    [CreateAssetMenu(menuName = Constants.CreateAssetMenuFolder + "/Figure Data")]
    public class FigureData : ScriptableObject
    {
        [field: SerializeField] public Shape Neutral { get; set; }
        [field: SerializeField] public Shape Charmed { get; set; }
        [field: SerializeField] public Shape Broken { get; set; }

        public List<Object> ObjectsUnloadOnDestroy { get; set; }

        private void OnDestroy()
        {
            if (ObjectsUnloadOnDestroy != null)
            {
                for (var i = 0; i < ObjectsUnloadOnDestroy.Count; i++)
                {
                    Addressables.Release(ObjectsUnloadOnDestroy[i]);
                }
            }
        }

        [Serializable]
        public class Shape
        {
            [field: SerializeField] public string DisplayName { get; set; }
            [field: SerializeField] public int Hp { get; set; }
            [field: SerializeField] public int Attack { get; set; }

            [field: SerializeField]
            [field: TextArea]
            public string Description { get; set; }

            [field: SerializeField] public List<CardData> OptionCards { get; set; }
            [field: SerializeField] public List<PassiveData> Passives { get; set; }
            [field: SerializeField] public FieldImageSet Face { get; set; }
        }
    }
}