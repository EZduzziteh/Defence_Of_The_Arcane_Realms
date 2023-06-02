namespace Gameplay
{
    using UnityEngine;


    public class Projectile : MonoBehaviour
    {
        [Header("Projectile Settings")]
        [SerializeField]
        public float launchForce = 100.0f;

        [SerializeField]
        public float damage = 25.0f;
        Rigidbody rb;
        AudioSource aud;

        [Header("Audio Settings")]
        [SerializeField]
        AudioClip fireProjectileSoundEffect;
        [SerializeField]
        AudioClip impactSoundEffect;
        bool hasDealtDamage = false;
        private void Awake()
        {
            aud = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        void Start()
        {
            AudioManager audMan = FindObjectOfType<AudioManager>();
            audMan.managedSoundSources.Add(aud);
            aud.volume = audMan.soundEffectVolume;
            //#TODO subscribe to audiomanager onchanged event

            rb = GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {

                if (hasDealtDamage == false)
                {
                    other.gameObject.GetComponent<Health>().TakeDamage(damage);

                    hasDealtDamage = true;
                    Destroy(gameObject);
                }

            }

          
        }
    }
}
