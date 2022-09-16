using ArcadeIdle.SaveSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using ArcadeIdle.ScriptableObjects;

namespace ArcadeIdle.Player
{
    [RequireComponent(typeof(CharacterController)), RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private CharacterController _controller;
        private Rigidbody _rb;
        
        private Vector3 _playerVelocity;

        [SerializeField] private PlayerSettingsSO playerSettings;

        [SerializeField] private Transform vfx;
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();
            _rb = GetComponent<Rigidbody>();
        }
        
        private void OnEnable()
        {
            Load();
        }

        private void OnDisable()
        {
            Save();
        }

        private void Save()
        {
            SaveManager.BinarySerialize("playerSettingsSpeed.arc", playerSettings.Speed);
            SaveManager.BinarySerialize("playerSettingsRotateSpeed.arc", playerSettings.RotateSpeed);
        }

        private void Load()
        {
            playerSettings.Speed = SaveManager.BinaryDeserialize<float>("playerSettingsSpeed.arc");
            playerSettings.RotateSpeed = SaveManager.BinaryDeserialize<float>("playerSettingsRotateSpeed.arc");
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
            _rb.velocity = Vector3.zero;
            _controller.enabled = false;
            playerSettings.CanMove = false;
        }
        
        public void StartMovement()
        {
            if(playerSettings.CanMove) return;
            _rb.velocity = Vector3.zero;
            _controller.enabled = true;
            playerSettings.CanMove = true;
        }
    }
}
