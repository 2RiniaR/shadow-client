using System.Collections.Generic;
using RineaR.Shadow.Rules;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RineaR.Shadow.Views
{
    public class FigureSelectView : MonoBehaviour
    {
        [SerializeField]
        private Button submit;

        [SerializeField]
        private CanvasGroup slotGroup;

        [SerializeField]
        private List<Button> slots;

        [SerializeField]
        private CanvasGroup entryGroup;

        [SerializeField]
        private List<Button> entries;

        [SerializeField]
        private Sprite entryDefaultImage;

        [SerializeField]
        private Sprite slotDefaultImage;

        private int? _currentSlot;

        public FigureSelectSystem System { get; set; }

        public void Initialize()
        {
            submit.OnClickAsObservable()
                .Subscribe(_ => System.Submit())
                .AddTo(this);

            for (var i = 0; i < slots.Count; i++)
            {
                var i1 = i;
                slots[i].OnClickAsObservable()
                    .Subscribe(_ => OnSlotSelected(i1))
                    .AddTo(this);
            }

            for (var i = 0; i < entries.Count; i++)
            {
                var i1 = i;
                entries[i].OnClickAsObservable()
                    .Subscribe(_ => OnEntrySelected(i1))
                    .AddTo(this);
            }

            System.ObserveEveryValueChanged(system => system.Entries)
                .Subscribe(_ => RefreshEntries())
                .AddTo(this);

            System.ObserveEveryValueChanged(system => system.Selections)
                .Subscribe(_ => RefreshSelections())
                .AddTo(this);

            ReadyToSelectSlot();
        }

        private void ReadyToSelectEntry()
        {
            slotGroup.interactable = false;
            entryGroup.interactable = true;
            EventSystem.current.SetSelectedGameObject(entries[0].gameObject);
        }

        private void ReadyToSelectSlot()
        {
            slotGroup.interactable = true;
            entryGroup.interactable = false;
            EventSystem.current.SetSelectedGameObject(slots[0].gameObject);
        }

        public void DeselectSlot()
        {
            _currentSlot = null;
            ReadyToSelectSlot();
        }

        public void SelectSlot(int index)
        {
            _currentSlot = index;
            ReadyToSelectEntry();
        }

        public void SetEntryToCurrentSlot(int index)
        {
            if (!_currentSlot.HasValue)
            {
                Debug.LogWarning("スロットを選択していません。");
                return;
            }

            System.SetSelection(_currentSlot.Value, System.Entries[index]);
        }

        private void OnSlotSelected(int index)
        {
            SelectSlot(index);
        }

        private void OnEntrySelected(int index)
        {
            SetEntryToCurrentSlot(index);
            DeselectSlot();
        }

        private void RefreshEntries()
        {
            for (var i = 0; i < entries.Count; i++)
            {
                var button = entries[i];
                var image = button.GetComponent<Image>();
                if (image)
                {
                    image.sprite = i < System.Entries.Count
                        ? System.Entries[i].FaceImage
                        : entryDefaultImage;
                }
            }
        }

        private void RefreshSelections()
        {
            for (var i = 0; i < slots.Count; i++)
            {
                var button = slots[i];
                var image = button.GetComponent<Image>();
                if (image)
                {
                    image.sprite = i < System.Selections.Count && System.Selections[i] != null
                        ? System.Selections[i].FaceImage
                        : slotDefaultImage;
                }
            }

            submit.interactable = System.CanSubmit();
        }
    }
}