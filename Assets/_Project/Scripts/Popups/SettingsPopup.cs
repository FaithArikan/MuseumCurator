using ArcadeIdle.ScriptableObjects;
using UnityEngine;

namespace ArcadeIdle.Popups
{
    public class SettingsPopup : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO gameSettingsSo;
        
        [SerializeField] private GameObject soundOn;
        [SerializeField] private GameObject soundOff;
        
        [SerializeField] private GameObject musicOn;
        [SerializeField] private GameObject musicOff;
        
        [SerializeField] private GameObject vibrationOn;
        [SerializeField] private GameObject vibrationOff;
        
        [SerializeField] private string linkedInURL = "https://www.linkedin.com/in/fatiharikan/";
        [SerializeField] private string githubURL = "https://github.com/FaithArikan";
        
        public void OpenLinkedInProfile()
        {
            Application.OpenURL($"{linkedInURL}");
        }
        
        public void OpenGithubProfile()
        {
            Application.OpenURL($"{githubURL}");
        }

        
        public void TapSound()
        {
            gameSettingsSo.ToggleSound();
            ChangeSoundImage(gameSettingsSo.IsSoundOn);
        }
        
        public void TapMusic()
        {
            gameSettingsSo.ToggleMusic();
            ChangeMusicImage(gameSettingsSo.IsMusicOn);
        }
        
        public void TapVibration()
        {
            gameSettingsSo.ToggleVibration();
            ChangeVibrationImage(gameSettingsSo.IsVibrationOn);
        }

        private void ChangeSoundImage(bool isSoundOn)
        {
            if (isSoundOn)
            {
                soundOn.SetActive(true);
                soundOff.SetActive(false);
            }
            else
            {
                soundOn.SetActive(false);
                soundOff.SetActive(true);
            }
        }
        
        private void ChangeMusicImage(bool isMusicOn)
        {
            if (isMusicOn)
            {
                musicOn.SetActive(true);
                musicOff.SetActive(false);
            }
            else
            {
                musicOn.SetActive(false);
                musicOff.SetActive(true);
            }
        }
        
        private void ChangeVibrationImage(bool isVibrationOn)
        {
            if (isVibrationOn)
            {
                vibrationOn.SetActive(true);
                vibrationOff.SetActive(false);
            }
            else
            {
                vibrationOn.SetActive(false);
                vibrationOff.SetActive(true);
            }
        }
    }
}