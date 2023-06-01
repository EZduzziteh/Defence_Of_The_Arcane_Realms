namespace Gameplay
{
    using UnityEngine;

    public class WindBlast : MonoBehaviour
    {
        [Header("Blast Settings")]
        public float blastRadius = 5.0f;
        public float knockbackForce = 100.0f;
        public float knockbackDelay = 2.0f;


        private void Start()
        {
            foreach (var contact in Physics.OverlapSphere(transform.position, blastRadius))
            {
                if (contact.tag == "Enemy")
                {/*
                  
                  Vector3 AB = B - A. Destination - Origin.

This is a direction and a distance. To have only the direction (and a distance of 1), you have to normalize it.

AB = AB.normalized OR AB.Normalize()

                  */

                    Vector3 knockbackDirection = (contact.transform.position - new Vector3(transform.position.x, contact.transform.position.y, transform.position.z)).normalized;

                    contact.GetComponent<Enemy_AI>().ApplyKnockback(knockbackDirection, knockbackForce, knockbackDelay);
                }
            }
        }

    }
}