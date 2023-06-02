namespace Gameplay
{
    using UnityEngine;

    public class Turret : MonoBehaviour
    {

        protected GameObject currentTarget;



        [Header("Upgrade Settings")]
        [SerializeField]
        protected bool isUpgradeable;
        [SerializeField]
        protected Turret upgradeTurret;
        [SerializeField]
        protected GameObject upgradeTurretVisualEffect;


        [Header("Turret Settings")]
        [SerializeField]
        protected float baseTimeBetweenShots = 1.0f;
        [SerializeField]
        protected float fireRate = 1.0f;
        [SerializeField]
        protected bool facesTarget = true;



        [SerializeField]
        protected float range = 10.0f;
        [SerializeField]
        protected int Price;
        [SerializeField]
        protected GameObject projectile;


        [Header("Audio Settings")]
        protected AudioSource aud;
        [SerializeField]
        protected AudioClip buildSound;
        [SerializeField]
        protected AudioClip fireSoundEffect;


        //references
        protected Range_Indicator rangeIndicator;
        protected Turret_Base turretBase;
        protected Turret_Pivot turretPivot;
        protected Projectile_Spawn_Point projectileSpawnPoint;

        //variables
        float fireTimer = 0.0f;


        private void Awake()
        {
            rangeIndicator = GetComponentInChildren<Range_Indicator>();
            turretBase = GetComponentInChildren<Turret_Base>();
            turretPivot = GetComponentInChildren<Turret_Pivot>();
            projectileSpawnPoint = GetComponentInChildren<Projectile_Spawn_Point>();
            aud = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            AudioManager audMan = FindObjectOfType<AudioManager>();
            audMan.managedSoundSources.Add(aud);
            aud.volume = audMan.soundEffectVolume;
            //#TODO subscribe to audiomanager onchanged event
            aud.clip = buildSound;
            aud.Play();
            GameObject temp = GameObject.Instantiate(upgradeTurretVisualEffect, transform.position, transform.rotation);
            Destroy(temp.gameObject, 5.0f);

            HideTurretRange();
        }



        // Update is called once per frame
        protected virtual void Update()
        {
            //if we do not currently have a target, or our current target has died, try to find a new target.
            if (currentTarget == null || currentTarget.GetComponent<Enemy_AI>().isAlive == false)
            {
                currentTarget = null;
                FindTargetInRadius();
            }
            else
            {
                //if we do have a currently alive target, check to make sure it is still in range.
                if (CheckIfTargetInRange())
                {
                    if (facesTarget)
                    {

                        //if its in range and we are suppposed to face target, look at it 
                        LookAtTarget();

                    }

                    //then fire when the turret is ready.
                    HandleFireTimer();
                }
                else
                {
                    //if targe is not in range, set our current target to be null.
                    currentTarget = null;
                }

            }
        }


        //Upgrades turret if able
        public bool TryUpgradeTurret(Tower_Base towerBase)
        {
            if (isUpgradeable)
            {
                Turret temp = GameObject.Instantiate(upgradeTurret, transform.position, transform.rotation);
                towerBase.SetNewTurret(temp);

                return true;
            }
            else
            {
                return false;
            }
        }


        //Checks to see if target is in range with a little bit of padding to account for the targets width.
        //returns true if target is in range, returns false if target is outside of range.
        private bool CheckIfTargetInRange()
        {
            float padding = 1.0f;

            if (Vector3.Distance(currentTarget.transform.position, transform.position) > range + padding)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //This handles the fire rate of the turret and is called in Update();
        private void HandleFireTimer()
        {
            //tick down our timer every frame
            fireTimer -= Time.deltaTime * fireRate;

            //if our timer is less than 0, fire our turret.
            if (fireTimer < 0.0f)
            {
                fireTimer = baseTimeBetweenShots;
                Fire();
            }
        }

        //his method instantiates a projectile at the specified spawn point.
        public virtual void Fire()
        {
            GameObject proj = GameObject.Instantiate(projectile,
                                                     projectileSpawnPoint.transform.position,
                                                     projectileSpawnPoint.transform.rotation);

        }

        //this method makes the turret look towards its target (compensating for distance to targeet)
        void LookAtTarget()
        {
            //Generate the desired target position, maintaining the height (y) of the turret itself 
            Vector3 targetPosition = new Vector3(currentTarget.transform.position.x,
                                                 turretBase.transform.position.y,
                                                 currentTarget.transform.position.z);
            //make the turret base rotate towards our desired position
            this.transform.LookAt(targetPosition);

            //Get distance from turret pivot to target;
            float targetDistance = Vector3.Distance(targetPosition, turretPivot.transform.position);

            //Make the turret pivot look towards the target, but compensating for distance (the farther away the targt is, the farther we have to look up)
            //#TODO Make this factor in the targets movement so we can lead shots.
            turretPivot.transform.LookAt(new Vector3(currentTarget.transform.position.x,
                                                   currentTarget.transform.position.y + targetDistance / 6,
                                                   currentTarget.transform.position.z)
                                        );
        }

        //In-Editor Only, Just Debug information to show turrets range.
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }

        //Checks in a radius around the turret to see if the target is within range.
        void FindTargetInRadius()
        {

            //Generate Sphere overlap based on range, from the turrets position.
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

            //Loop through all of the colliders that were hit, letting this loop through all possible hits is costly so #TODO optimize this later (find the enemy closest to the nexus and in range of the turret and choose that one)
            foreach (var hitCollider in hitColliders)
            {
                //if the hit collider was an enemy
                if (hitCollider.tag == "Enemy")
                {
                    //and if they are alive
                    if (hitCollider.GetComponent<Enemy_AI>().isAlive)
                    {
                        //set thecurrent target to be that hit collider
                        currentTarget = hitCollider.gameObject;
                        break; //stop running once we find a valid target

                    }
                }
            }



        }


        //this method makes a sphere show up around the turret, displaying the turrets range to the player.
        public void ShowTurretRange()
        {
            if (rangeIndicator)
            {

                rangeIndicator.gameObject.SetActive(true);
                rangeIndicator.transform.localScale = new Vector3(range * 2.0f, range * 2.0f, range * 2.0f);

            }
            else
            {
                rangeIndicator = GetComponentInChildren<Range_Indicator>();
            }

        }

        //this method hides the sphere that was shown in the above method, hiding the turrets range to clog the screen less.
        public void HideTurretRange()
        {

            if (rangeIndicator)
            {


                rangeIndicator.gameObject.SetActive(false);
                rangeIndicator.transform.localScale = new Vector3(range * 2.0f, range * 2.0f, range * 2.0f);

            } else
            {
                rangeIndicator = GetComponentInChildren<Range_Indicator>();
            }


        }

        //Getters

        public int getTurretPrice()
        {
            return Price;
        }
        public Turret GetUpgradeTurret()
        {
            return upgradeTurret;
        }
    }
}