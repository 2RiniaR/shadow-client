using System.Collections.Generic;
using RineaR.Shadow.Master;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RineaR.Shadow.UI
{
    public class UnitSelectView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text title;

        [SerializeField]
        private CanvasGroup submitScreen;

        [SerializeField]
        private Button submit;

        [SerializeField]
        private List<Image> holders;

        [SerializeField]
        private List<Button> entries;

        [SerializeField]
        private Sprite holderDefault;

        private List<int> _items;

        [Inject] private IMasterRepository MasterRepository { get; set; }

        public void Initialize()
        {
            _items = new List<int>();

            submit.OnClickAsObservable().Subscribe(_ => Submit()).AddTo(this);
            for (var unitID = 0; unitID < entries.Count; unitID++)
            {
                var button = entries[unitID];
                var id = unitID;
                button.OnClickAsObservable().Subscribe(_ => SelectUnit(id)).AddTo(this);
            }

            Refresh();
        }

        public void SelectUnit(int unitID)
        {
            _items.Add(unitID);
            Refresh();
        }

        private void Refresh()
        {
            for (var unitID = 0; unitID < entries.Count; unitID++)
            {
                var button = entries[unitID];
                var image = button.GetComponent<Image>();
                if (image) image.sprite = MasterRepository.GetUnitByID(unitID).faceImage;
            }

            for (var itemNumber = 0; itemNumber < holders.Count; itemNumber++)
            {
                var image = holders[itemNumber];

                if (itemNumber >= _items.Count)
                    image.sprite = holderDefault;
                else
                {
                    var unitID = _items[itemNumber];
                    image.sprite = MasterRepository.GetUnitByID(unitID).faceImage;
                }
            }
        }

        public void Submit()
        {
        }
    }
}