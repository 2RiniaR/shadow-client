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

        [Inject] public IMasterRepository Master { get; }

        public MainScene Scene { get; }

        public void OnEnterPhase()
        {
            if (Scene.Session.HasStateAuthority)
            {
                var battle = Scene.Session.Runner.Spawn(Scene.BattlePrefab);

                // フィールドを生成する
                var fieldPrefab = Master.GetFieldByID(Scene.Session.FieldID.Value).Field;
                var field = Scene.Session.Runner.Spawn(
                    fieldPrefab,
                    onBeforeSpawned: (runner, obj) => { obj.transform.SetParent(battle.transform); }
                );

                // プレイヤーを生成する
                foreach (var client in Scene.Session.GetAllClients())
                {
                    switch (client.Role)
                    {
                        case SessionClientRole.None:
                            break;
                        case SessionClientRole.Player:
                            Scene.Session.Runner.Spawn(
                                Scene.PlayerPrefab,
                                inputAuthority: client.Object.InputAuthority,
                                onBeforeSpawned: (runner, obj) =>
                                {
                                    var player = obj.GetComponent<BattlePlayer>();
                                    player.Battle = battle;
                                    player.transform.SetParent(battle.transform);
                                });
                            break;
                        case SessionClientRole.Audience:
                            Scene.Session.Runner.Spawn(
                                Scene.AudiencePrefab,
                                inputAuthority: client.Object.InputAuthority,
                                onBeforeSpawned: (runner, obj) =>
                                {
                                    var player = obj.GetComponent<BattlePlayer>();
                                    player.Battle = battle;
                                    player.transform.SetParent(battle.transform);
                                });
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        public void OnExitPhase()
        {
        }
    }
}