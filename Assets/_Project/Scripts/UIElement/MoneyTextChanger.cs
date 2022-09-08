using ArcadeIdle.Helpers;
using ArcadeIdle.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace ArcadeIdle.UIElement
{
    public class MoneyTextChanger : MonoBehaviour
    {
        [Header("Blue Money")]
        [SerializeField] private TextMeshProUGUI blueMoneyAmountText;
        [SerializeField] private ResourceSO blueMoney;
        [Header("Purple Money")]
        [SerializeField] private TextMeshProUGUI purpleMoneyAmountText;
        [SerializeField] private ResourceSO purpleMoney;

        private void Awake()
        {
            OnAmountChange();
        }
        public void OnAmountChange()
        {
            blueMoneyAmountText.text = Utils.FormatCash(blueMoney.Amount);
            purpleMoneyAmountText.text = Utils.FormatCash(purpleMoney.Amount);
        }
    }
}