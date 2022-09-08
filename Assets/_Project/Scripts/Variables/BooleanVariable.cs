using ArcadeIdle.Helpers.Events;
using UnityEngine;

namespace ArcadeIdle.Variables
{
    [CreateAssetMenu(menuName = "Variables/Boolean")]
    public class BooleanVariable : ScriptableObject
    {
        [SerializeField] private bool boolValue;
        [SerializeField] private GameEvent onValueChanged;

        public bool Value
        {
            get => boolValue;
            set
            {
                boolValue = value;
                if(onValueChanged != null) onValueChanged.Invoke();
            }
        }
    }
}