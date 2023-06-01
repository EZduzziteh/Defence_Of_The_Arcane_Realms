namespace Gameplay
{
    using UnityEngine;

    public class Turret_Flamer : Turret
    {
        [Header("Flamer Settings")]
        [SerializeField]
        float damage = 5.0f;
        [SerializeField]
        float flameRadius = 1.0f;
        [SerializeField]
        GameObject FlameParticles;

        bool isFiring = false;

        protected override void Start()
        {
            base.Start();
            FlameParticles.SetActive(false);
            //Debug.DrawLine(base.projectileSpawnPoint.transform.position, base.projectileSpawnPoint.transform.position + base.projectileSpawnPoint.transform.forward * range, Color.red, 15.0f);

        }

        protected override void Update()
        {
            base.Update();
            if (base.currentTarget)
            {
                if (base.currentTarget.GetComponent<Enemy_AI>().isAlive)
                {
                    FlameParticles.SetActive(true);
                    aud.clip = fireSoundEffect;
                    if (!isFiring)
                    {
                        isFiring = true;
                        aud.Play();
                        aud.loop = true;
                    }
                }
                else
                {
                    FlameParticles.SetActive(false);
                    isFiring = false;
                    aud.Stop();
                }

            }
            else
            {
                if (isFiring)
                {
                    FlameParticles.SetActive(false);
                    isFiring = false;
                    aud.Stop();
                }
            }

        }

        private void OnDrawGizmosSelected()
        {
            if (base.projectileSpawnPoint)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(base.projectileSpawnPoint.transform.position, base.projectileSpawnPoint.transform.position + base.projectileSpawnPoint.transform.forward * range);
            }
        }
        public override void Fire()
        {

            Collider[] hitColliders = Physics.OverlapCapsule(base.projectileSpawnPoint.transform.position, base.projectileSpawnPoint.transform.position + base.projectileSpawnPoint.transform.forward * range, flameRadius);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.tag == "Enemy")
                {
                    hitCollider.GetComponent<Enemy_AI>().GetHealth().TakeDamage(damage);
                }
            }

        }
    }
}