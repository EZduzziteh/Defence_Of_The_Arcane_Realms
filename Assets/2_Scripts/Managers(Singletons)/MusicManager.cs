namespace Gameplay
{
    using UnityEngine;

    public class MusicManager : MonoBehaviour
    {

        AudioSource aud;
        // Start is called before the first frame update
        void Awake()
        {
            aud = GetComponent<AudioSource>();
        }

        private void Start()
        {
            AudioManager audMan = FindObjectOfType<AudioManager>();
            audMan.managedMusicSources.Add(aud);
            aud.volume = audMan.musicVolume;
        }

    }
}