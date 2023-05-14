using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace RineaR.Shadow.Master.Spreadsheet
{
    [CreateAssetMenu(menuName = ProjectConstants.CreateAssetMenuFolder + "/Master Asset Registry")]
    public class MasterAssetRegistry : ScriptableObject
    {
        [field: SerializeField] public List<FigureAsset> Figures { get; set; }

        [CanBeNull]
        private Dictionary<string, FigureAsset> _figureLookup;

        private void CreateLookup()
        {
            _figureLookup = new Dictionary<string, FigureAsset>();
            foreach (var figure in Figures)
            {
                _figureLookup.Add(figure.ID, figure);
            }
        }

        [CanBeNull]
        public FigureAsset FindFigureAsset(string id)
        {
            if (_figureLookup == null) CreateLookup();
            return _figureLookup!.TryGetValue(id, out var item) ? item : null;
        }

        [Serializable]
        public class FigureAsset
        {
            [field: SerializeField] public string ID { get; set; }
            [field: SerializeField] public Sprite FaceImage { get; set; }
        }
    }
}