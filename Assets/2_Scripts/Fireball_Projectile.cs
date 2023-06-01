namespace Gameplay
{
    using UnityEngine;

    public class Fireball_Projectile : MonoBehaviour
    {



        [Header("Explosion Settings")]
        public float explosionRadius = 5.0f;
        public float explosionDamage = 100.0f;
        public GameObject explosionEffect;

        bool hasMadeContact = false;
        private void OnTriggerEnter(Collider other)
        {
            if (hasMadeContact == false)
            {
                GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);

                hasMadeContact = true;

                foreach (var contact in Physics.OverlapSphere(transform.position, explosionRadius))
                {
                    if (contact.tag == "Enemy")
                    {
                        contact.GetComponent<Enemy_AI>().GetHealth().TakeDamage(explosionDamage);
                    }
                }
                Destroy(this.gameObject, 0.5f);
            }
        }
    }
}
