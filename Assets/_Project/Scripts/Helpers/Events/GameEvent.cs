using System.Collections.Generic;
using UnityEngine;

namespace ArcadeIdle.Helpers.Events
{
    [CreateAssetMenu(menuName = "GameEvent/DefaultGameEvent")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener.Listener> _listeners = new ();

        public void Invoke()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].Invoke();
            }
        }

        public void RegisterListener(GameEventListener.Listener listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener.Listener listener)
        {
            _listeners.Remove(listener);
        }
    }
}