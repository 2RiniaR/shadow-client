using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace RineaR.Shadow.Master
{
    [CreateAssetMenu(menuName = "shadow/MasterSet")]
    public class MasterSet : ScriptableObject, IMasterRepository
    {
        public List<UnitSetting> units;

        public void Fetch()
        {
            // do nothing
        }

        public UnitSetting[] GetUnits()
        {
            return units.ToArray();
        }

        [CanBeNull]
        public UnitSetting GetUnitByID(int id)
        {
            return units.Find(unit => unit.id == id);
        }
    }
}