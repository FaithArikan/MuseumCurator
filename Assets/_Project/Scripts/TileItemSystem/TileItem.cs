using System.Collections;
using ArcadeIdle.Helpers;
using ArcadeIdle.Helpers.Events;
using ArcadeIdle.ScriptableObjects;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArcadeIdle.TileItemSystem
{
    public class TileItem : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private BoxCollider col;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image moneyTypeImage;
        
        [SerializeField] private int itemPrice;
        [SerializeField] private ResourceSO priceResource;

        [SerializeField] private GameEvent onItemOpened;

        private void Awake()
        {
            OnItemVisible();
        }
        
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
            onItemOpened.Invoke();
            
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