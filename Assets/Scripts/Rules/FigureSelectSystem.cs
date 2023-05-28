using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using RineaR.Shadow.Master;
using RineaR.Shadow.Network;
using UniRx;
using UnityEngine;

namespace RineaR.Shadow.Rules
{
    /// <summary>
    ///     フィギュアを選択するシステム。
    /// </summary>
    public class FigureSelectSystem : IDisposable
    {
        /// <summary>
        ///     選択するフィギュアの数。
        /// </summary>
        public const int NumberOfSelection = 4;

        [ItemNotNull]
        private List<FigureData> _entries;

        private AsyncSubject<Unit> _onConfirmed;

        [ItemCanBeNull]
        private List<FigureData> _selections;

        /// <summary>
        ///     選択可能なフィギュア。
        /// </summary>
        [ItemNotNull]
        public ReadOnlyCollection<FigureData> Entries => new(_entries);

        /// <summary>
        ///     選択されたフィギュア。
        /// </summary>
        [ItemCanBeNull]
        public ReadOnlyCollection<FigureData> Selections => new(_selections);

        public IObservable<Unit> OnConfirmed => _onConfirmed;

        public Session Session { get; set; }
        public IMasterRepository Master { get; set; }

        public void Dispose()
        {
            _onConfirmed?.Dispose();
        }

        public void Initialize()
        {
            _onConfirmed = new AsyncSubject<Unit>();
            _selections =
                new List<FigureData>(Enumerable.Range(0, NumberOfSelection).Select(_ => (FigureData)null));
            _entries = new List<FigureData>(Master.GetFigures());
        }

        public void SetSelection(int slot, [CanBeNull] FigureData entry)
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

            if (!Session.LocalClient)
            {
                Debug.LogWarning($"{typeof(SessionClient)} が存在しません。");
                return;
            }

            _onConfirmed.OnNext(Unit.Default);
            _onConfirmed.OnCompleted();
        }
    }
}