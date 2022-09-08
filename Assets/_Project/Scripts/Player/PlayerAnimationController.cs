using UnityEngine;

namespace ArcadeIdle.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnMovementBegan()
        {
            _animator.SetBool(IsMoving, true);
        }

        public void OnMovementEnded()
        {
            _animator.SetBool(IsMoving, false);
        }
    }
}