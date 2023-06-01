namespace Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;

    class Enemy_Stats
    {

    }

    public class Enemy_AI : MonoBehaviour
    {
        protected NavMeshAgent agent;

        Enemy_Stats stats;
        //#TODO Refactor this into a separate "stats" class? scriptable object amaybe
        [Header("Combat Settings")]

        [SerializeField]
        protected float timeBetweenAttacks = 1.5f;
        [SerializeField]
        protected float damage = 100;
        [SerializeField]
        protected float deathSinkRate = 1.0f;
        [SerializeField]
        protected float timeBeforeDeathSink = 2.0f;
        [SerializeField]
        protected float attackDoDamageDelay = 0.5f;
        Health health;
        [SerializeField]
        GameObject floatingTextPrefab;


        [Header("Reward Settings")]
        [SerializeField]
        protected int goldValue = 1;

        [Header("Speed Settings")]
        [SerializeField]
        protected float baseSpeed = 2.0f;



        protected float currentSpeed = 2.0f;
        protected float speedModifier = 1.0f;


        protected float currentSpeedModifier = 1.0f;

        [Header("Sound Settings")]
        [SerializeField]
        protected List<AudioClip> deathSoundEffects = new List<AudioClip>();


        protected float speedDebuffTimer;
        protected bool isSpeedDebuffed = false;

        protected float attackTimer;

        public bool isAlive = true;
        protected float timeSinceDeath = 0.0f;

        protected GameObject target;

        protected Animator anim;
        protected bool isInAttackRange;
        Rigidbody rb;
        protected AudioSource aud;

        protected virtual void Awake()
        {
            GatherReferences();
        }
        // Start is called before the first frame update
        protected virtual void Start()
        {
            SetDefaultValues();
            AcquireTarget();
            MoveToTarget();
        }
        protected virtual void Update()
        {
            if (isSpeedDebuffed)
            {
                speedDebuffTimer -= Time.deltaTime;
                if (speedDebuffTimer <= 0)
                {
                    isSpeedDebuffed = false;
                    speedDebuffTimer = 0;
                    ResetAgentSpeed();
                }
            }

            if (!isAlive)
            {
                if (timeSinceDeath >= timeBeforeDeathSink)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - (deathSinkRate * Time.deltaTime), transform.position.z);
                }
                else
                {
                    timeSinceDeath += Time.deltaTime;
                }
            }
            else
            {
                if (isInAttackRange)
                {
                    if (target)
                    {

                        attackTimer -= Time.deltaTime;
                        if (attackTimer <= 0)
                        {
                            Attack();
                        }
                    }
                }


            }

        }



        public void SetDefaultValues()
        {
            currentSpeed = baseSpeed;
            currentSpeedModifier = speedModifier;
            agent.speed = currentSpeed * currentSpeedModifier;
            anim.SetBool("isWalking", true);
            rb.isKinematic = true;
        }
        public void GatherReferences()
        {

            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            health = GetComponent<Health>();
            agent = GetComponent<NavMeshAgent>();
            aud = GetComponent<AudioSource>();
            AudioManager audMan = FindObjectOfType<AudioManager>();
            audMan.managedEnemySources.Add(aud);
            aud.volume = audMan.enemyEffectVolume;
        }
        public virtual void MoveToTarget()
        {

            if (target)
            {

                agent.SetDestination(target.transform.position);

            }




        }
        public virtual void AcquireTarget()
        {
            target = FindObjectOfType<Nexus>().gameObject;

            if (target == null)
            {
                isInAttackRange = false;
                anim.SetBool("isWalking", false);
                agent.isStopped = true;

            }
            else
            {
                isInAttackRange = false;
                anim.SetBool("isWalking", true);
                agent.isStopped = false;
                attackTimer = 0.0f;
                MoveToTarget();
            }

        }
        public virtual void SetNewTarget(GameObject newTarget)
        {
            target = newTarget;
        }


        public void StartSpeedDebuff(float duration, float amount)
        {
            if (isAlive)
            {
                speedDebuffTimer = duration;
                isSpeedDebuffed = true;
                currentSpeedModifier = amount;
                UpdateAgentSpeed();

            }

            GetComponent<FrostChanger>().setFrostAmount(0.25f);

        }

        public void UpdateAgentSpeed()
        {
            if (isAlive)
            {
                currentSpeed = baseSpeed * currentSpeedModifier;
                if (agent)
                {
                    agent.speed = currentSpeed;
                }
            }
        }

        public void ResetAgentSpeed()
        {
            if (isAlive)
            {
                currentSpeed = baseSpeed;
                currentSpeedModifier = speedModifier;
                UpdateAgentSpeed();
            }

            GetComponent<FrostChanger>().setFrostAmount(0.0f);
        }

        public Health GetHealth()
        {
            return health;
        }

        public void HandleDeath()
        {
            if (isAlive)
            {
                if (floatingTextPrefab)
                {
                    ShowFloatingText();
                }

                FindObjectOfType<PlayerController>().AddGold(goldValue);
                FindObjectOfType<WaveManager>().enemiesLeft--;
                GetComponent<Health>().HideHealthBar();
                anim.SetBool("isDead", true);
                anim.SetBool("isWalking", false);
                agent.isStopped = true;
                Destroy(agent);
                Destroy(GetComponent<Collider>());

                aud.clip = GetRandomDeathSoundEffect();
                aud.Play();
                isAlive = false;
                timeSinceDeath = 0.0f;
                Destroy(this.gameObject, 5.0f);
            }

        }

        public void ShowFloatingText()
        {
            GameObject temp = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            temp.GetComponent<TextMesh>().text = "+" + goldValue;
        }


        private void OnTriggerEnter(Collider other)
        {


            if (!isInAttackRange)
            {
                if (isAlive)
                {
                    if (target.GetComponent<Nexus>())
                    {
                        if (other.gameObject.tag == "Nexus")
                        {

                            isInAttackRange = true;
                            anim.SetBool("isWalking", false);
                            agent.isStopped = true;
                            attackTimer = 0.0f;
                            return;

                        }
                    }

                    if (target.GetComponent<Destructable>())
                    {
                        if (other.gameObject.tag == "Walls")
                        {
                            isInAttackRange = true;
                            anim.SetBool("isWalking", false);
                            agent.isStopped = true;
                            attackTimer = 0.0f;
                            return;

                        }
                    }


                }
            }
        }

        private void Attack()
        {
            anim.SetTrigger("Attack");
            attackTimer = timeBetweenAttacks;
            StartCoroutine(dealDamageDelay());
        }


        public virtual void DealDamageToTarget()
        {

            if (target)
            {

                Nexus nexus = target.GetComponent<Nexus>();

                nexus.TakeDamage(damage);
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

        public AudioClip GetRandomDeathSoundEffect()
        {
            System.Random rand = new System.Random();

            int randomNum = rand.Next(0, deathSoundEffects.Count - 1);

            return deathSoundEffects[randomNum];
        }



        internal void ApplyKnockback(Vector3 knockbackDirection, float knockbackForce, float knockbackDelay)
        {
            agent.isStopped = true;
            agent.enabled = false;

            GetComponent<Collider>().isTrigger = false;
            StartCoroutine(KnockbackDelay(knockbackDelay));
            rb.isKinematic = false;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }


        //ienumerators
        IEnumerator dealDamageDelay()
        {

            yield return new WaitForSeconds(attackDoDamageDelay);

            if (isAlive)
            {
                DealDamageToTarget();
            }
        }
        IEnumerator KnockbackDelay(float delay)
        {

            yield return new WaitForSeconds(delay);



            agent.enabled = true;
            agent.isStopped = false;
            GetComponent<Collider>().isTrigger = true;
            if (target)
            {
                agent.SetDestination(target.transform.position);
            }
            else
            {
                AcquireTarget();
            }
            rb.isKinematic = true;
        }

        //getters and setters
        internal float GetCurrentSpeedModifier()
        {
            return currentSpeedModifier;
        }

    }
}