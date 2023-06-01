namespace Gameplay
{
    using UnityEngine;


    public class Sieger : Enemy_AI
    {
        Destructable closestDestructable;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            closestDestructable = FindObjectOfType<Destructable>();
        }

        protected override void Update()
        {
            base.Update();

            if (target.GetComponent<Destructable>().GetIsFalling())
            {
                isInAttackRange = false;
                anim.SetBool("isWalking", true);
                agent.isStopped = false;
                attackTimer = 0.0f;
                AcquireTarget();
            }
        }


        public override void AcquireTarget()
        {

            float shortest = Mathf.Infinity;
            closestDestructable = null;

            foreach (Destructable dest in FindObjectsOfType<Destructable>())
            {
                if (!dest.GetIsFalling())
                {
                    float distanceBetween = Vector3.Distance(transform.position, dest.transform.position);

                    if (distanceBetween < shortest)
                    {
                        shortest = distanceBetween;
                        closestDestructable = dest;
                    }
                }

            }

            if (closestDestructable)
            {
                target = closestDestructable.gameObject;

                MoveToTarget();
                return;
            }
            else
            {
                base.AcquireTarget();
            }



        }


        public override void DealDamageToTarget()
        {
            if (target)
            {
                Destructable destructable = target.GetComponent<Destructable>();

                if (destructable.GetIsFalling())
                {
                    Debug.Log("here");
                    target = null;
                    closestDestructable = null;
                    isInAttackRange = false;
                    anim.SetBool("isWalking", false);
                    agent.isStopped = false;
                    attackTimer = 0.0f;
                    AcquireTarget();

                }
                else
                {

                    destructable.TakeDamage(damage);
                    if (destructable.GetIsFalling())
                    {
                        Debug.Log("here2");
                        closestDestructable = null;
                        isInAttackRange = false;

                        attackTimer = 0.0f;
                        AcquireTarget();
                    }

                }
            }
            else
            {
                isInAttackRange = false;
                anim.SetBool("isWalking", true);
                agent.isStopped = false;
                attackTimer = 0.0f;

                AcquireTarget();
            }
        }



    }
}