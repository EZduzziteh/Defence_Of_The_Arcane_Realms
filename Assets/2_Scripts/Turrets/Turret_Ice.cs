namespace Gameplay
{
    using UnityEngine;

    public class Turret_Ice : Turret
    {
        [SerializeField]
        float damagePerTick = 1.0f;
        [SerializeField]
        float slowTime = 2.0f; //the duration that this will last once unit leaves the radius of the turret (reapplied every tick)
        [SerializeField]
        float slowAmount = 0.5f; //half speed



        public override void Fire()
        {
            // base.Fire();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.tag == "Enemy")
                {
                    hitCollider.gameObject.GetComponent<Health>().TakeDamage(damagePerTick);
                    if (slowAmount <= hitCollider.gameObject.GetComponent<Enemy_AI>().GetCurrentSpeedModifier()) //only apply slow if it is slower or as slow as the current speed debuff (stops overlapping with worse debuffs)
                    {
                        hitCollider.gameObject.GetComponent<Enemy_AI>().StartSpeedDebuff(slowTime, slowAmount);
                    }

                }
            }

        }
    }
}
