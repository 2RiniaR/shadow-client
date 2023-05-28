using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RineaR.Shadow.Master
{
    [CreateAssetMenu(menuName = Constants.CreateAssetMenuFolder + "/Addressable Master")]
    public class AddressableMaster : ScriptableObject, IMasterRepository
    {
        private readonly Dictionary<string, CardData> _cards = new();
        private readonly Dictionary<string, FieldData> _fields = new();
        private readonly Dictionary<string, FigureData> _figures = new();
        private readonly Dictionary<string, PassiveData> _passives = new();

        private void OnDestroy()
        {
            foreach (var card in _cards)
            {
                Addressables.Release(card);
            }

            foreach (var field in _fields)
            {
                Addressables.Release(field);
            }

            foreach (var figure in _figures)
            {
                Addressables.Release(figure);
            }

            foreach (var passive in _passives)
            {
                Addressables.Release(passive);
            }
        }

        public async UniTask FetchAsync(CancellationToken cancellationToken = default)
        {
            await UniTask.WhenAll(new UniTask[]
            {
                Addressables.LoadAssetsAsync<CardData>("Cards", card => _cards.Add(card.name, card))
                    .ToUniTask(cancellationToken: cancellationToken),
                Addressables.LoadAssetsAsync<FieldData>("Fields", field => _fields.Add(field.name, field))
                    .ToUniTask(cancellationToken: cancellationToken),
                Addressables.LoadAssetsAsync<FigureData>("Figures", figure => _figures.Add(figure.name, figure))
                    .ToUniTask(cancellationToken: cancellationToken),
                Addressables.LoadAssetsAsync<PassiveData>("Passives", passive => _passives.Add(passive.name, passive))
                    .ToUniTask(cancellationToken: cancellationToken),
            });
        }

        public IEnumerable<FigureData> GetFigures()
        {
            return _figures.Values;
        }

        [CanBeNull]
        public FigureData GetFigureByID(string id)
        {
            return _figures.TryGetValue(id, out var item) ? item : null;
        }

        public IEnumerable<CardData> GetCards()
        {
            return _cards.Values;
        }

        [CanBeNull]
        public CardData GetCardByID(string id)
        {
            return _cards.TryGetValue(id, out var item) ? item : null;
        }

        public IEnumerable<FieldData> GetFields()
        {
            return _fields.Values;
        }

        [CanBeNull]
        public FieldData GetFieldByID(string id)
        {
            return _fields.TryGetValue(id, out var item) ? item : null;
        }

        public IEnumerable<PassiveData> GetPassives()
        {
            return _passives.Values;
        }

        [CanBeNull]
        public PassiveData GetPassiveByID(string id)
        {
            return _passives.TryGetValue(id, out var item) ? item : null;
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            _cards.Clear();
            _fields.Clear();
            _figures.Clear();
            _passives.Clear();
        }

        [ContextMenu("Debug fetch")]
        private async void DebugFetch()
        {
            Clear();
            await FetchAsync();
            Debug.Log(
                $"[Assets loaded] {_cards.Count} cards, {_fields.Count} fields, {_figures.Count} figures, {_passives.Count} passives");
        }
    }
}