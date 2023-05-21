namespace RineaR.Shadow.Common.Phases
{
    public sealed class PhaseSwitch
    {
        public IPhase Current { get; private set; }

        public void Set(IPhase phase)
        {
            Current?.OnExitPhase();
            Current = phase;
            Current?.OnEnterPhase();
        }
    }
}