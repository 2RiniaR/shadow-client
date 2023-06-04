using Fusion;

namespace RineaR.Shadow.Battles
{
    /// <summary>
    ///     試合の観戦者。
    /// </summary>
    public class BattleAudience : NetworkBehaviour
    {
        [Networked] public Battle Battle { get; set; }

        public override void Spawned()
        {
            if (Battle)
            {
                Battle.Audiences.Add(this);
                transform.SetParent(Battle.transform);
            }
        }
    }
}