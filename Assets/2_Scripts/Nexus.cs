namespace Gameplay
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.Collections;
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

        public bool nexusDestroyed = false;

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
            GameObject temp = Instantiate(damageParticles.gameObject, transform.position, transform.rotation);
         
            temp.SetActive(true);
            //temp.GetComponent<ParticleSystem>().Play();
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
            nexusDestroyed = true;
            StartCoroutine(NexusDestroyedDelay(3.0f));
            

        }
        IEnumerator NexusDestroyedDelay(float delay)
        {

            yield return new WaitForSeconds(delay);


            FindObjectOfType<UI_Manager>().HandleGameOver();


        }

    }


}