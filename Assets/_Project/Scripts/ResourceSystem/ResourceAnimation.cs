using DG.Tweening;
using UnityEngine;

namespace ArcadeIdle.ResourceSystem
{
    public class ResourceAnimation : MonoBehaviour
    {
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Plane"))
            {
                _rb.velocity = Vector3.zero;
                _rb.useGravity = false;
                GetComponent<BoxCollider>().isTrigger = true;
                transform.DOMoveY(transform.localPosition.y + 0.2f, 1f).OnComplete
                        (() => transform.DOMoveY(transform.localPosition.y - 0.2f, 1f)).
                    SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                transform.DOLocalRotate(new Vector3(0, 360f, 0),3f).
                    SetRelative().SetLoops(-1).SetEase(Ease.Linear);
            }
        }
    }
}