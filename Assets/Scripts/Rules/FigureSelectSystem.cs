using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using RineaR.Shadow.Master;
using RineaR.Shadow.Network;
using UnityEngine;
using Zenject;

namespace RineaR.Shadow.Rules
{
    /// <summary>
    ///     フィギュアを選択するシステム。
    /// </summary>
    public class FigureSelectSystem : MonoBehaviour
    {
        /// <summary>
        ///     選択するフィギュアの数。
        /// </summary>
        public const int NumberOfSelection = 4;

        [ItemNotNull]
        private List<FigureSetting> _entries;

        [ItemCanBeNull]
        private List<FigureSetting> _selections;

        /// <summary>
        ///     選択可能なフィギュア。
        /// </summary>
        [ItemNotNull]
        public ReadOnlyCollection<FigureSetting> Entries => new(_entries);

        /// <summary>
        ///     選択されたフィギュア。
        /// </summary>
        [ItemCanBeNull]
        public ReadOnlyCollection<FigureSetting> Selections => new(_selections);

        [Inject] private SessionConnector Connector { get; set; }
        [Inject] private IMasterRepository Master { get; set; }

        public void Initialize()
        {
            _selections =
                new List<FigureSetting>(Enumerable.Range(0, NumberOfSelection).Select(_ => (FigureSetting)null));
            _entries = new List<FigureSetting>(Master.GetFigures());
        }

        public void SetSelection(int slot, [CanBeNull] FigureSetting entry)
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

            Connector.Client.BattleSettings.SetFigures(Selections.Select(figure => figure!.ID).ToArray());
        }
    }
}