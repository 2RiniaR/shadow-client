using RineaR.MadeHighlow.Network;
using UnityEngine;
using Zenject;

namespace RineaR.MadeHighlow.Common
{
    public class GameManager : MonoBehaviour
    {
        [Inject] private RemoteManager RemoteManager { get; set; }
        [Inject] private MatchingWindow MatchingWindow { get; set; }

        private void Start()
        {
            RemoteManager.Initialize();

            MatchingWindow.Initialize();
            MatchingWindow.Open();
        }
    }
}