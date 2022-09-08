using ArcadeIdle.Helpers.Events;
using UnityEngine;

namespace ArcadeIdle.Variables
{
    [CreateAssetMenu(menuName = "Variables/Int")]
    public class IntVariable : ScriptableObject
    {
        [SerializeField] private int intValue;
        [SerializeField] private GameEvent onValueChanged;

        public int Value
        {
            get => intValue;
            set
            {
                intValue = value;
                if (onValueChanged != null) onValueChanged.Invoke();
            }
        }
    }
}