namespace Gameplay
{
    using UnityEngine;

    public class Healer : MonoBehaviour
    {
        [Header("Heal Settings")]
        [SerializeField]
        float timeBetweenHeals = 2.0f;
        [SerializeField]
        float healAmount = 25.0f;
        [SerializeField]
        float healRadius = 2.0f;
        [SerializeField]
        AudioClip healSoundEffect;

        float healTimer;
        AudioSource aud;

        // Start is called before the first frame update
        void Start()
        {
            healTimer = timeBetweenHeals;
            aud = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            healTimer -= Time.deltaTime;
            if (healTimer <= 0)
            {
                foreach (var unit in Physics.OverlapSphere(transform.position, healRadius))
                {
                    if (unit.GetComponent<Enemy_AI>())
                    {
                        unit.GetComponent<Health>().HealDamage(healAmount);
                        healTimer = timeBetweenHeals;
                        aud.clip = healSoundEffect;
                        aud.Play();

                    }
                }
            }
        }
    }
}
