using System;
using System.Collections;
using ArcadeIdle.Helpers;
using ArcadeIdle.Helpers.Events;
using ArcadeIdle.SaveSystem;
using ArcadeIdle.ScriptableObjects;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArcadeIdle.TileItemSystem
{
    [SelectionBase]
    public class TileItem : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private BoxCollider col;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image moneyTypeImage;
        
        [SerializeField] private int itemPrice;
        [SerializeField] private bool isItemOpened;
        private int _beginItemPrice;
        [SerializeField] private ResourceSO priceResource;

        [SerializeField] private GameEvent onItemOpened;
        public event Action<int> OnItemOpenedAction;

        private void Awake()
        {
            _beginItemPrice = itemPrice;
        }

        private void OnEnable()
        {
            Load();
            if (!isItemOpened)
            {
                OnItemVisible();
            }
            else
            {
                OnItemEnable();
            }
        }

        private void OnDisable()
        {
            Save();
        }

        #region Save-Load
        
        private void Save()
        {
            SaveManager.BinarySerialize($"{gameObject.name}isItemOpened.arc", isItemOpened);
            SaveManager.BinarySerialize($"{gameObject.name}itemPrice.arc", itemPrice);
        }

        private void Load()
        {
            isItemOpened = SaveManager.BinaryDeserialize<bool>($"{gameObject.name}isItemOpened.arc");
            itemPrice = SaveManager.BinaryDeserialize<int>($"{gameObject.name}itemPrice.arc");
        }
        
        #endregion

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Decrease();
            }
        }

        private void OnItemVisible()
        {
            canvas.enabled = true;
            text.text = Utils.FormatCash(itemPrice);
            moneyTypeImage.sprite = priceResource.MoneySprite;
        }
        
        private void OnItemEnable()
        {
            OnItemOpenedAction?.Invoke(_beginItemPrice);
            
            GetComponentInChildren<Canvas>().enabled = false;
            col.enabled = false;
            
            ObjectTweenAnimation();
        }
        
        private void ObjectTweenAnimation()
        {
            transform.DOMoveY(transform.localPosition.y + 1, 1f).OnComplete
                (() => transform.DOMoveY(transform.localPosition.y - 1, 1f));
            transform.DORotate(new Vector3(0f, 360f, 0f), 2f, RotateMode.FastBeyond360);
        }

        private void Decrease()
        {
            if (itemPrice <= 0)
            {
                OnItemEnable();
                return;
            }
            int count;
            if(priceResource.Amount <= 0) return;
            
            if (itemPrice > 100)
            {
                count = itemPrice / 100;
            }
            else
            {
                count = 1;
            }

            if (itemPrice < 0) return;
            if (priceResource.Amount <= 0) return;

            StartCoroutine(DecreaseCoroutine(count));
        }

        IEnumerator DecreaseCoroutine(int count)
        {
            itemPrice -= count;
            text.text = Utils.FormatCash(itemPrice);
            priceResource.Amount -= count;
            yield break;
        }
    }
}