using System;
using ArcadeIdle.Helpers.Events;
using ArcadeIdle.SaveSystem;
using UnityEngine;
using UnityEngine.Events;

namespace ArcadeIdle.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Paint", order = 0)]
    public class PaintSO : ScriptableObject
    {
        [SerializeField] private PaintType paintType;
        [SerializeField] private Material material;
        
        [SerializeField] private int amount;
        [SerializeField] private GameEvent onResourceValueChanged;

        public UnityAction<Material> OnPaintSelected = delegate { };
        
        public int Amount
        {
            get => amount;
            set
            {
                if (amount == value) return;
                onResourceValueChanged.Invoke();
            }
        }

        public void SelectPaint()
        {
            Debug.Log(Amount);
            OnPaintSelected?.Invoke(material);
            amount--;
            Debug.Log(Amount);
        }
    }
}