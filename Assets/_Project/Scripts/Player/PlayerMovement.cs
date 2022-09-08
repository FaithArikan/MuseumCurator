using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using ArcadeIdle.ScriptableObjects;

namespace ArcadeIdle.Player
{
    [RequireComponent(typeof(CharacterController)), RequireComponent(typeof(PlayerInput)), 
     RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private PlayerInput _playerInput;
        private CharacterController _controller;
        
        private Vector3 _playerVelocity;

        [SerializeField] private PlayerSettingsSO playerSettings;

        [SerializeField] private Transform vfx;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _controller = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (!playerSettings.CanMove) return;
            CharMove();
            CharLook();
        }
        
        private void CharMove()
        {
            Vector2 input = _playerInput.actions["MoveAction"].ReadValue<Vector2>();
            Vector3 move = new Vector3(input.x, 0, input.y);
            
            _controller.Move(move * Time.deltaTime * playerSettings.Speed);
            if (Mathf.Abs(input.x) > 0.1 || Mathf.Abs(input.y) > 0.1)
            {
                playerSettings.IsMoving = true;
            }
            else
            {
                playerSettings.IsMoving = false;
            }
        }
        
        private void CharLook()
        {
            Vector2 input = _playerInput.actions["LookAction"].ReadValue<Vector2>();
            Vector3 look = new Vector3(input.x, 0, input.y);
            if (look == Vector3.zero) return;
            vfx.rotation = Quaternion.Slerp(vfx.rotation, Quaternion.LookRotation(look), 
                playerSettings.RotateSpeed * Time.deltaTime );
        }

        public void StopMovement()
        {
            if(!playerSettings.CanMove) return;
            playerSettings.CanMove = false;
        }
        public void StartMovement()
        {
            if(playerSettings.CanMove) return;
            playerSettings.CanMove = true;
        }

    }
}
