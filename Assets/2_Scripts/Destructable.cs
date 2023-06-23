namespace Gameplay
{
    using System.Collections;
    using UnityEngine;

    public class Destructable : MonoBehaviour
    {
        [SerializeField]
        ParticleSystem damageParticles;
        [SerializeField]
        ParticleSystem destroyedParticles;
        [SerializeField]
        AudioClip takeDamageSoundEffect;
        [SerializeField]
        AudioClip destroyedSoundEffect;
        [SerializeField]
        GameObject destructableReplacement;


        [SerializeField]
        float replacementSpawnDelay = 3.0f;

        AudioSource aud;
        Health healthRef;

        bool isFalling = false;
        [SerializeField]
        float fallSpeed = 3.0f;

        Rigidbody rb;
        AudioManager audMan;


        public void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            audMan = FindObjectOfType<AudioManager>();
            healthRef = GetComponent<Health>();
            aud = GetComponent<AudioSource>();
            audMan.managedSoundSources.Add(aud);
            aud.volume = audMan.soundEffectVolume;
        }

        private void Update()
        {
            if (isFalling)
            {
                rb.MovePosition(new Vector3(transform.position.x, transform.position.y - fallSpeed * Time.deltaTime, transform.position.z));
            }
        }

        public bool GetIsFalling()
        {
            return isFalling;
        }
        public void TakeDamage(float damage)
        {

            if (isFalling)
            {
                return;
            }

            healthRef.TakeDamage(damage);
            aud.clip = takeDamageSoundEffect;
            aud.Play();
            damageParticles.gameObject.SetActive(true);
            damageParticles.Play();


        }

        public void HandleDestruction()
        {
            if (!isFalling)
            {
                aud.clip = destroyedSoundEffect;
                aud.Play();

                destroyedParticles.gameObject.SetActive(true);
                destroyedParticles.transform.parent = null;
                destructableReplacement.transform.parent = null;
                Destroy(GetComponent<Collider>());
                isFalling = true;

                StartCoroutine(spawnNewDelay());



            }
        }
        IEnumerator spawnNewDelay()
        {

            yield return new WaitForSeconds(replacementSpawnDelay);


            SpawnNew();

        }

        public void SpawnNew()
        {

            destructableReplacement.SetActive(true);
            Destroy(destroyedParticles.gameObject);
            Destroy(this.gameObject);
        }

    }
}