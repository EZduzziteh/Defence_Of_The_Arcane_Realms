namespace Gameplay
{
    using UnityEngine;
    public class Nexus : MonoBehaviour
    {
        [Header("VFX Settings")]
        [SerializeField]
        GameObject deathParticlesPrefab;
        [SerializeField]
        ParticleSystem damageParticles;
        [SerializeField]
        GameObject runesParticle;


        AudioSource aud;
        [Header("SFX Settings")]
        [SerializeField]
        AudioClip takedamageSoundEffect;

        [SerializeField]
        AudioClip destroyedSoundEffect;

        Health healthRef;

        private void Awake()
        {

            healthRef = GetComponent<Health>();
            aud = GetComponent<AudioSource>();
        }
        private void Start()
        {
            AudioManager audman = FindObjectOfType<AudioManager>();
            aud.volume = audman.soundEffectVolume;
            audman.managedSoundSources.Add(aud);
            //#TODO subscribe to audiomanager event

        }
        public void TakeDamage(float damage)
        {


            healthRef.TakeDamage(damage);
            aud.clip = takedamageSoundEffect;
            aud.Play();
            GameObject temp = Instantiate(damageParticles.gameObject, damageParticles.transform.position, damageParticles.transform.rotation);
         
            temp.SetActive(true); 
            Destroy(temp.gameObject, 3.0f);



        }

        internal void DestroyNexus()
        {
            if (deathParticlesPrefab)
            {
                GameObject.Instantiate(deathParticlesPrefab, transform.position, transform.rotation);
            }

            aud.clip = destroyedSoundEffect;
            aud.Play();

            FindObjectOfType<UI_Manager>().HandleGameOver();
        }
    }
}