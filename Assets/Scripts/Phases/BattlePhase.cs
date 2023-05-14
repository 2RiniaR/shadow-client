using Fusion;
using RineaR.Shadow.Battles;
using RineaR.Shadow.Common.Phases;
using RineaR.Shadow.Master;
using RineaR.Shadow.Network;
using Zenject;

namespace RineaR.Shadow.Phases
{
    public class BattlePhase : IPhaseHandler
    {
        private NetworkRunner _networkRunner;
        [Inject] public SessionConnector Connector { get; private set; }
        [Inject] public AppSettings AppSettings { get; private set; }
        [Inject] public IMasterRepository Master { get; }

        public Battle Battle { get; private set; }

        public void Start()
        {
            if (!Connector.Client) return;

            _networkRunner = Connector.Client.Runner;
            var server = Connector.Client.Server;

            Battle = _networkRunner.Spawn(AppSettings.BattlePrefab);

            // フィールドを生成する
            var fieldPrefab = Master.GetFieldByID(server.BattleSettings.FieldID.Value).Field;
            var field = _networkRunner.Spawn(fieldPrefab);
            Battle.Use(field);

            // プレイヤーを生成する
            foreach (var client in server.Clients)
            {
                if (client.BattleSettings.JoinAsPlayer)
                {
                    var player = _networkRunner.Spawn(AppSettings.PlayerPrefab);
                    Battle.Join(player);
                }
                else
                {
                    var audience = _networkRunner.Spawn(AppSettings.AudiencePrefab);
                    Battle.Join(audience);
                }
            }

            Battle.Initialize();
        }

        public void Finish()
        {
            _networkRunner.Despawn(Battle.Object);
        }
    }
}