using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UniRx;

namespace RineaR.Shadow.Common.Phases
{
    public sealed class PhaseSwitch<TPhaseName> : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly ReactiveProperty<TPhaseName> _currentPhaseName;
        private readonly Dictionary<TPhaseName, IPhaseHandler> _phaseHandlers = new();

        public PhaseSwitch(TPhaseName initial)
        {
            _currentPhaseName = new ReactiveProperty<TPhaseName>(initial);
        }

        public TPhaseName CurrentPhaseName => _currentPhaseName.Value;
        [CanBeNull] public IPhaseHandler CurrentPhase { get; set; }

        public void Dispose()
        {
            _currentPhaseName?.Dispose();
            _compositeDisposable?.Dispose();
        }

        public void Set(TPhaseName phaseName)
        {
            _currentPhaseName.Value = phaseName;
        }

        public void RegisterHandler(TPhaseName phaseName, IPhaseHandler phaseHandler)
        {
            _phaseHandlers.TryAdd(phaseName, phaseHandler);
        }

        public void Initialize()
        {
            _currentPhaseName.Subscribe(OnPhaseChanged).AddTo(_compositeDisposable);
        }

        private void OnPhaseChanged(TPhaseName phaseName)
        {
            CurrentPhase?.Finish();
            CurrentPhase = null;

            if (_phaseHandlers.TryGetValue(phaseName, out var handler)) CurrentPhase = handler;
            CurrentPhase?.Start();
        }
    }
}