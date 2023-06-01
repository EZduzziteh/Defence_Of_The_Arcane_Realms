
namespace Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioManager : MonoBehaviour
    {

        [Range(0, 1)] public float musicVolume = 0.5f;

        [Range(0, 1)] public float soundEffectVolume = 0.5f;

        [Range(0, 1)] public float enemyEffectVolume = 0.5f;

        public List<AudioSource> managedMusicSources = new List<AudioSource>();

        public List<AudioSource> managedSoundSources = new List<AudioSource>();

        public List<AudioSource> managedEnemySources = new List<AudioSource>();

        // Start is called before the first frame update
        void Awake()
        {
            RefreshVolumes();
        }

        public void RefreshVolumes()
        {

            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            soundEffectVolume = PlayerPrefs.GetFloat("SoundEffectVolume");
            soundEffectVolume = PlayerPrefs.GetFloat("EnemyEffectVolume");


            foreach (AudioSource aud in managedEnemySources)
            {
                aud.volume = enemyEffectVolume;
            }
            foreach (AudioSource aud in managedMusicSources)
            {
                aud.volume = musicVolume;
            }
            foreach (AudioSource aud in managedSoundSources)
            {
                aud.volume = soundEffectVolume;
            }
        }

        public void SetMusicVolume(float amount)
        {
            PlayerPrefs.SetFloat("MusicVolume", amount);
            RefreshVolumes();
        }
        public void SetSoundEffectVolume(float amount)
        {
            PlayerPrefs.SetFloat("SoundEffectVolume", amount);
            RefreshVolumes();
        }
        public void SetEnemyyVolume(float amount)
        {
            PlayerPrefs.SetFloat("EnemyEffectVolume", amount);
            RefreshVolumes();
        }
    }
}
