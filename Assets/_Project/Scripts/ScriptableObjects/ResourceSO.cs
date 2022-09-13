using ArcadeIdle.Helpers.Events;
using UnityEngine;

namespace ArcadeIdle.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Core/Resource", order = 0)]
    public class ResourceSO : ScriptableObject
    {
        [SerializeField] private ResourceTypes resourceType;
        [SerializeField] private int amount;
        [SerializeField] private int maximum = int.MaxValue;
        [SerializeField] private int minimum;
        [SerializeField] private GameEvent onResourceValueChanged;

        [SerializeField] private Sprite moneySprite;

        public int Amount
        {
            get => amount;
            set
            {
                if (amount == value) return;
                amount = Mathf.Clamp(value, minimum, maximum);
                onResourceValueChanged.Invoke();
            }
        }

        public Sprite MoneySprite => moneySprite;
    }
}