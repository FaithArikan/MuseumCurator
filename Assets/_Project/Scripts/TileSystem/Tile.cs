using System;
using System.Collections;
using ArcadeIdle.Helpers.Events;
using ArcadeIdle.ScriptableObjects;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ArcadeIdle.TileSystem
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileSO tileSo;
        
        [Header("Game Events and Actions")]
        // Trying different events and actions to see which one is better
        [SerializeField] private GameEvent onTileOpened;
        public event Action<int> OnTileOpenAction;
        public event Action OnTileSpawnObjectsAction;
        public UnityAction<bool> OnColliderAction = delegate { };

        
        [Header("UI Elements")] 
        [SerializeField] private Image moneyTypeImage;
        
        [Header("Tile Objects")]
        [SerializeField] private GameObject ground;

        public TileSO TileSo => tileSo;


        private void OpenTile()
        {
            TileSo.IsOpenable = true;
            GetComponentInChildren<Canvas>().enabled = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Decrease();
            }
        }
        
        private void OnEnable()
        {
            if (TileSo.IsOpenable)
            {
                GetComponentInChildren<Canvas>().enabled = false;
            }
            else
            {
                GetComponentInChildren<Canvas>().enabled = true;
            }
            
            ground.gameObject.SetActive(false);
            OnColliderAction?.Invoke(true);
            
            OnTileOpenAction?.Invoke(TileSo.TilePrice);
            moneyTypeImage.sprite = TileSo.TilePriceResource.MoneySprite;
        }

        private void OnTileEnable()
        {
            onTileOpened.Invoke();
            OnColliderAction?.Invoke(false);
            OnTileSpawnObjectsAction?.Invoke();

            TileSo.IsTileOpened = true;
            
            GetComponentInChildren<Canvas>().enabled = false;
            ground.gameObject.SetActive(true);
        }

        private void Decrease()
        {
            if (TileSo.IsTileOpened) return;

            if (TileSo.TilePrice <= 0)
            {
                OnTileEnable();
                return;
            }
            int count;
            if(TileSo.TilePriceResource.Amount <= 0) return;
            
            if (TileSo.TilePrice > 100)
            {
                count = TileSo.TilePrice / 100;
            }
            else
            {
                count = 1;
            }

            if (TileSo.TilePrice < 0) return;
            if (TileSo.TilePriceResource.Amount <= 0) return;

            StartCoroutine(DecreaseCoroutine(count));
        }

        IEnumerator DecreaseCoroutine(int count)
        {
            TileSo.TilePrice -= count;
            OnTileOpenAction?.Invoke(TileSo.TilePrice);
            TileSo.TilePriceResource.Amount -= count;
            yield break;
        }
    }
}