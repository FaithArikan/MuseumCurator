using System;
using UnityEngine;

namespace ArcadeIdle.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Core/GameSettings", fileName = "GameSettings")]
    public class GameSettingsSO : ScriptableObject
    {
        [SerializeField] private bool isSoundOn = true;
        [SerializeField] private bool isMusicOn = true;
        [SerializeField] private bool isVibrationOn = true;
        
        public event Action<bool> SoundSettingChanged;
        public event Action<bool> MusicSettingChanged;
        public event Action<bool> VibrationSettingChanged;

        public bool IsSoundOn
        {
            get => isSoundOn;
            set
            {
                isSoundOn = value;
                SoundSettingChanged?.Invoke(isSoundOn);
            }
        }

        public bool IsMusicOn
        {
            get => isMusicOn;
            set
            {
                isMusicOn = value;
                MusicSettingChanged?.Invoke(isMusicOn);
            }
        }

        public bool IsVibrationOn
        {
            get => isVibrationOn;
            set
            {
                isVibrationOn = value;
                VibrationSettingChanged?.Invoke(isVibrationOn);
            }
        }

        public void ToggleSound()
        {
            IsSoundOn = !IsSoundOn;
        }
        
        public void ToggleMusic()
        {
            IsMusicOn = !IsMusicOn;
        }

        public void ToggleVibration()
        {
            IsVibrationOn = !IsVibrationOn;
        }
    }
}