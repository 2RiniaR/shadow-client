using RineaR.Shadow.Battles;
using UnityEngine;

namespace RineaR.Shadow
{
    [CreateAssetMenu(menuName = "shadow/AppSettings")]
    public class AppSettings : ScriptableObject
    {
        [SerializeField]
        private BattlePlayer playerPrefab;

        [SerializeField]
        private BattleAudience audiencePrefab;

        [SerializeField]
        private Battle battlePrefab;

        public BattlePlayer PlayerPrefab => playerPrefab;
        public BattleAudience AudiencePrefab => audiencePrefab;
        public Battle BattlePrefab => battlePrefab;
    }
}