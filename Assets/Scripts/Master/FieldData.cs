using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RineaR.Shadow.Master
{
    [CreateAssetMenu(menuName = Constants.CreateAssetMenuFolder + "/Field Data")]
    public class FieldData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField] public AssetReferenceGameObject Field { get; set; }
    }
}