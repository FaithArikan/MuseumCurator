using ArcadeIdle.Helpers.Events;
using UnityEngine;

namespace ArcadeIdle.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Core/PlayerSettings", fileName = "PlayerSettings")]
    public class PlayerSettingsSO : ScriptableObject
    {
        [SerializeField] private bool canMove;
        [SerializeField] private bool isMoving;
        
        [Header("Speed")]
        [SerializeField] private float speed;
        [Header("Rotate Speed")]
        [SerializeField] private float rotateSpeed;

        [SerializeField] private GameEvent onMoveValueChanged;
        [SerializeField] private GameEvent onMovementBegan;
        [SerializeField] private GameEvent onMovementEnded;

        public bool CanMove
        {
            get => canMove;
            set
            {
                canMove = value;
                onMoveValueChanged.Invoke();
            }
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public float RotateSpeed
        {
            get => rotateSpeed;
            set => rotateSpeed = value;
        }

        public bool IsMoving
        {
            get => isMoving;
            set
            {
                isMoving = value;
                GameEvent MovementEvent() => IsMoving ? onMovementBegan : onMovementEnded;
                MovementEvent().Invoke();
            }
        }
    }
}