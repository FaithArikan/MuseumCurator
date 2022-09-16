using ArcadeIdle.Helpers.Events;
using UnityEngine;

namespace ArcadeIdle.PaintSystem
{
    public class PaintPhysics : MonoBehaviour
    {
        [SerializeField] private GameEvent onPaintButtonShow;
        [SerializeField] private GameEvent onPaintButtonHide;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                onPaintButtonShow.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                onPaintButtonHide.Invoke();
            }
        }
    }
}