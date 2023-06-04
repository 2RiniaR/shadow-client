using System;
using Cysharp.Threading.Tasks;
using RineaR.Shadow.Battles;
using RineaR.Shadow.Battles.Fields;
using RineaR.Shadow.Common.Phases;
using RineaR.Shadow.Network;
using UnityEngine;

namespace RineaR.Shadow.Scenes.Main
{
    public class BattlePhase : IPhase
    {
        public BattlePhase(MainScene scene)
        {
            Scene = scene;
        }

        public MainScene Scene { get; }

        public async void OnEnterPhase()
        {
            if (Scene.Session.HasStateAuthority)
            {
                var fieldRef = Scene.Master.GetFieldByID("001_marble-space").Field;
                var fieldPrefab = await fieldRef.LoadAssetAsync().ToUniTask();

                var battle = Scene.Session.Runner.Spawn(Scene.BattlePrefab);

                // フィールドを生成する
                Scene.Session.Runner.Spawn(
                    fieldPrefab,
                    onBeforeSpawned: (runner, obj) =>
                    {
                        var field = obj.GetComponent<Field>();
                        field.Battle = battle;
                    }
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
                                });
                            break;
                        case SessionClientRole.Audience:
                            Scene.Session.Runner.Spawn(
                                Scene.AudiencePrefab,
                                inputAuthority: client.Object.InputAuthority,
                                onBeforeSpawned: (runner, obj) =>
                                {
                                    var audience = obj.GetComponent<BattleAudience>();
                                    audience.Battle = battle;
                                });
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                Debug.Log("complete!");
            }
        }

        public void OnExitPhase()
        {
        }
    }
}