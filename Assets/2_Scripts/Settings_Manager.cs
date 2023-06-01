namespace Gameplay
{
    using UnityEngine;
    using UnityEngine.UI;

    public class Settings_Manager : MonoBehaviour
    {
        AudioManager audMan;
        [SerializeField]
        Slider sfxSlider;
        [SerializeField]
        Slider enemySlider;
        [SerializeField]
        Slider musicSlider;
        private void Start()
        {
            audMan = FindObjectOfType<AudioManager>();

            musicSlider.value = audMan.musicVolume;
            sfxSlider.value = audMan.soundEffectVolume;
            enemySlider.value = audMan.enemyEffectVolume;
        }



        public void SetMusicVolume(float amount)
        {
            PlayerPrefs.SetFloat("MusicVolume", amount);
            audMan.RefreshVolumes();
        }
        public void SetSoundEffectVolume(float amount)
        {
            PlayerPrefs.SetFloat("SoundEffectVolume", amount);

            audMan.RefreshVolumes();
        }
        public void SetEnemyyVolume(float amount)
        {
            PlayerPrefs.SetFloat("EnemyEffectVolume", amount);

            audMan.RefreshVolumes();
        }
    }
}