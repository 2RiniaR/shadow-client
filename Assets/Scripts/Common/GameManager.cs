using RineaR.Shadow.UI;
using UnityEngine;
using Zenject;

namespace RineaR.Shadow.Common
{
    public class GameManager : MonoBehaviour
    {
        [Inject] private MatchingView MatchingView { get; set; }
        [Inject] private UnitSelectView UnitSelectView { get; set; }

        private void Start()
        {
            MatchingView.Initialize();
            MatchingView.Open();
        }
    }
}