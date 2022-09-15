using System.Collections.Generic;
using ArcadeIdle.Helpers;
using ArcadeIdle.ScriptableObjects;
using UnityEngine;

namespace ArcadeIdle.AudioSystem
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [SerializeField] private List<AudioClip> backgroundMusicAudioClips;
        [SerializeField] private List<AudioClip> tileOpenSoundAudioClips;

        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource soundAudioSource;

        [SerializeField] private GameSettingsSO gameSettings;

        #region ActionAssignment

        private void Awake()
        {
            AssignActions();
        }

        private void OnDisable()
        {
            UnAssignActions();
        }

        private void AssignActions()
        {
            gameSettings.MusicSettingChanged += GameSettingsOnMusicSettingChanged;
            gameSettings.SoundSettingChanged += GameSettingsOnSoundSettingChanged;
        }


        private void UnAssignActions()
        {
            gameSettings.MusicSettingChanged -= GameSettingsOnMusicSettingChanged;
            gameSettings.SoundSettingChanged -= GameSettingsOnSoundSettingChanged;
        }


        #endregion

        public void OnTileOpened()
        {
            GameSettingsOnSoundSettingChanged(gameSettings.IsSoundOn);
        }

        public void OnGameStarted()
        {
            GameSettingsOnMusicSettingChanged(gameSettings.IsMusicOn);
        }
        
        private void GameSettingsOnSoundSettingChanged(bool isSound)
        {
            if (isSound)
            {
                if (!soundAudioSource.isPlaying)
                {
                    soundAudioSource.clip = tileOpenSoundAudioClips[Random.Range(0, tileOpenSoundAudioClips.Count)];
                    soundAudioSource.loop = false;
                    soundAudioSource.Play();
                }
                else
                {
                    soundAudioSource.Play();
                }
            }
            else
            {
                SoundStop();
            }
        }

        private void GameSettingsOnMusicSettingChanged(bool isMusic)
        {
            if (isMusic)
            {
                if (!musicAudioSource.isPlaying)
                {
                    musicAudioSource.clip = backgroundMusicAudioClips[Random.Range(0, backgroundMusicAudioClips.Count)];
                    musicAudioSource.loop = true;
                    musicAudioSource.Play();
                }
                else
                {
                    musicAudioSource.Play();
                }
            }
            else
            {
                MusicStop();
            }
        }

        private void MusicStop()
        {
            musicAudioSource.Stop();
        }
        
        private void SoundStop()
        {
            soundAudioSource.Stop();
        }

    }
}