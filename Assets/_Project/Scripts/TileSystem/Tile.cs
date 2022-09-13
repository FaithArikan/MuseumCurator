using System;
using System.Collections;
using System.Collections.Generic;
using ArcadeIdle.Helpers.Events;
using ArcadeIdle.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ArcadeIdle.TileSystem
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private string tileName;
        [SerializeField] private bool isBeginTile;
        [SerializeField] private bool isOpenableNow;
        [SerializeField] private bool isTileOpened;

        [SerializeField] private int tilePrice;

        [Header("Game Events and Actions")]
        // Trying different events and actions to see which one is better
        [SerializeField] private GameEvent onTileOpened;
        public event Action<int> OnTileOpenAction;
        public event Action OnTileSpawnObjectsAction;
        public UnityAction<bool> OnColliderAction = delegate { };
        
        //GUI
        [SerializeField] private TextMeshProUGUI nameText;
        
        [Header("UI Elements")] 
        [SerializeField] private Canvas tileCanvas;
        [SerializeField] private Canvas nameCanvas;
        [SerializeField] private Image moneyTypeImage;
        
        [Header("Tile Objects")]
        [SerializeField] private GameObject ground;
        
        //TILE FEATURES
        [SerializeField] private SectionTypes section;
        
            
        [SerializeField] private bool tileObjectAnim = true;

        public List<GameObject> tileObjects;
        public List<Vector3> tileObjectsPositions;

        [Header("Tile Features")]
        [SerializeField] private ResourceSO tilePriceResource;
        [SerializeField] private TileTypes tileType;

        public bool TileObjectAnim
        {
            get => tileObjectAnim;
            set => tileObjectAnim = value;
        }
        
        private void OpenTileForInteraction()
        {
            isOpenableNow = true;
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
            nameCanvas.enabled = false;
            
            if (tilePrice == -1)
            {
                tilePrice = Random.Range(100, 2500);
            }
            
            if (tileType == TileTypes.Water) return;

            if (isBeginTile)
            {
                isOpenableNow = false;
                isTileOpened = true;
                tileCanvas.enabled = false;
                ground.gameObject.SetActive(true);
                OnColliderAction?.Invoke(false);
                OnTileSpawnObjectsAction?.Invoke();
                return;
            }
            
            OnColliderAction?.Invoke(true);
            isTileOpened = false;
                
            if (isOpenableNow)
            {
                tileCanvas.enabled = true;
            }
            else
            {
                tileCanvas.enabled = false;
            }
            ground.gameObject.SetActive(false);

            
            OnTileOpenAction?.Invoke(tilePrice);
            moneyTypeImage.sprite = tilePriceResource.MoneySprite;
        }
        
         private void OnValidate()
         {
             if (string.IsNullOrEmpty(tileName))
             {
                 nameText.text = "default";
                 nameText.color = Color.magenta;
             }
             else
             {
                 nameText.text = tileName;
                 nameText.color = Color.yellow;
             }
             while (tileObjectsPositions.Count < tileObjects.Count)
             {
                 tileObjectsPositions.Add(new Vector3());
             }
         }

        private void OnTileEnable()
        {
            onTileOpened.Invoke();
            OnColliderAction?.Invoke(false);
            OnTileSpawnObjectsAction?.Invoke();

            isTileOpened = true;
            
            GetComponentInChildren<Canvas>().enabled = false;
            ground.gameObject.SetActive(true);
        }

        private void Decrease()
        {
            if (isTileOpened) return;

            if (tilePrice <= 0)
            {
                OnTileEnable();
                return;
            }
            int count;
            if(tilePriceResource.Amount <= 0) return;
            
            if (tilePrice > 100)
            {
                count = tilePrice / 100;
            }
            else
            {
                count = 1;
            }

            if (tilePrice < 0) return;
            if (tilePriceResource.Amount <= 0) return;

            StartCoroutine(DecreaseCoroutine(count));
        }

        IEnumerator DecreaseCoroutine(int count)
        {
            tilePrice -= count;
            OnTileOpenAction?.Invoke(tilePrice);
            tilePriceResource.Amount -= count;
            yield break;
        }
    }
}