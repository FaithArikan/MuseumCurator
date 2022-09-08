using ArcadeIdle.Helpers.Events;
using UnityEngine;

namespace ArcadeIdle.Variables
{
    [CreateAssetMenu(menuName = "Variables/Float")]
    public class FloatVariable : ScriptableObject
    {
        [SerializeField] private float floatValue;
        [SerializeField] private GameEvent onValueChanged;

        public float Value
        {
            get => floatValue;
            set
            {
                floatValue = value;
                if (onValueChanged != null) onValueChanged.Invoke();
            }
        }
    }
}