using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using RineaR.Shadow.Master;
using RineaR.Shadow.Network;
using UnityEngine;
using Zenject;

namespace RineaR.Shadow.Units
{
    public class UnitSelectSystem : MonoBehaviour
    {
        public const int NumberOfSelection = 4;

        [ItemNotNull]
        private List<UnitSetting> _entries;

        [ItemCanBeNull]
        private List<UnitSetting> _selections;

        [ItemNotNull] public ReadOnlyCollection<UnitSetting> Entries => new(_entries);
        [ItemCanBeNull] public ReadOnlyCollection<UnitSetting> Selections => new(_selections);

        [Inject] private SessionConnector Connector { get; set; }
        [Inject] private IMasterRepository Master { get; set; }

        public void Initialize()
        {
            _selections = new List<UnitSetting>(Enumerable.Range(0, NumberOfSelection).Select(_ => (UnitSetting)null));
            _entries = new List<UnitSetting>(Master.GetUnits());
        }

        public void SetSelection(int slot, [CanBeNull] UnitSetting entry)
        {
            if (slot is < 0 or >= NumberOfSelection) return;
            _selections[slot] = entry;
        }

        public bool CanSubmit()
        {
            return Selections.All(selection => selection != null);
        }

        public void Submit()
        {
            if (!CanSubmit())
            {
                Debug.LogWarning("未選択のスロットがあります。");
                return;
            }

            if (!Connector.Client)
            {
                Debug.LogWarning($"{typeof(SessionClient)} が存在しません。");
                return;
            }

            Connector.Client.RPC_SubmitUnits(Selections.Select(unit => unit!.id).ToArray());
        }
    }
}