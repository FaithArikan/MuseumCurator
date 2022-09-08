using System;
using UnityEngine;
using UnityEngine.Events;

namespace ArcadeIdle.Helpers.Events
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private Listener[] listeners;

        private void OnEnable()
        {
            foreach (Listener listener in listeners)
            {
                listener.Register();
            }
        }

        private void OnDisable()
        {
            foreach (Listener listener in listeners)
            {
                listener.UnRegister();
            }
        }
        [Serializable]
        public struct Listener
        {
            public GameEvent[] events;
            public UnityEvent response;
            public void Register()
            {
                foreach (GameEvent e in events) e.RegisterListener(this);
            }

            public void UnRegister()
            {
                foreach (GameEvent e in events) e.UnregisterListener(this);
            }

            public void Invoke()
            {
                response.Invoke();
            }
        }
    }
}