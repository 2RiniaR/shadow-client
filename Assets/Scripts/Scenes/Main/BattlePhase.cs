using System;
using RineaR.Shadow.Battles;
using RineaR.Shadow.Common.Phases;
using RineaR.Shadow.Master;
using RineaR.Shadow.Network;
using Zenject;

namespace RineaR.Shadow.Scenes.Main
{
    public class BattlePhase : IPhase
    {
        public BattlePhase(MainScene scene)
        {
            Scene = scene;
        }

        [Inject] public Session Session { get; private set; }
        [Inject] public IMasterRepository Master { get; }

        public MainScene Scene { get; }

        public Battle Battle { get; private set; }

        public void OnEnterPhase()
        {
            Battle = Session.Runner.Spawn(Scene.BattlePrefab);

            // フィールドを生成する
            var fieldPrefab = Master.GetFieldByID(Session.BattleSettings.FieldID.Value).Field;
            var field = Session.Runner.Spawn(fieldPrefab);
            Battle.Use(field);

            // プレイヤーを生成する
            foreach (var client in Session.GetAllClients())
            {
                switch (client.Settings.Role)
                {
                    case BattleClientRole.None:
                        break;
                    case BattleClientRole.Player:
                        var player = Session.Runner.Spawn(Scene.PlayerPrefab);
                        Battle.Join(player);
                        break;
                    case BattleClientRole.Audience:
                        var audience = Session.Runner.Spawn(Scene.AudiencePrefab);
                        Battle.Join(audience);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Battle.Initialize();
        }

        public void OnExitPhase()
        {
            Session.Runner.Despawn(Battle.Object);
        }
    }
}