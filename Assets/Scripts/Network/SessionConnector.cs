using Cysharp.Threading.Tasks;
using Fusion;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RineaR.Shadow.Network
{
    /// <summary>
    ///     セッションへの接続を行うクライアント。常時存在する。
    /// </summary>
    [RequireComponent(typeof(NetworkRunner))]
    public class SessionConnector : MonoBehaviour
    {
        private const string DefaultRoomName = "Default";

        private NetworkRunner _localRunner;

        [CanBeNull]
        private SessionClient _session;

        [CanBeNull]
        public SessionClient Client
        {
            get
            {
                if (_session) return _session;

                if (!_localRunner.IsRunning) return null;

                _session = _localRunner.GetPlayerObject(_localRunner.LocalPlayer)?.GetComponent<SessionClient>();
                return _session;
            }
        }

        private void Awake()
        {
            _localRunner = GetComponent<NetworkRunner>();
        }

        public async UniTask JoinSession()
        {
            await _localRunner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.AutoHostOrClient,
                SessionName = DefaultRoomName,
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = _localRunner.GetComponent<NetworkSceneManagerDefault>(),
            });
        }
    }
}