namespace Gameplay
{
    using UnityEngine;

    public class Tower_Base : MonoBehaviour
    {
        [SerializeField]
        GameObject turretSpawnPoint;
        public Turret currentTurret;
        public int turretLevel = 0;


        public void BuildTurret(Turret turret)
        {
            currentTurret = GameObject.Instantiate(turret, turretSpawnPoint.transform.position, turretSpawnPoint.transform.rotation);
            turretLevel = 1;
        }

        public void UpgradeTurret()
        {
            if (currentTurret.TryUpgradeTurret(this))
            {
                Debug.Log("Turret upgraded!");
                turretLevel++;
            }
            else
            {
                Debug.Log("Turret can not be upgraded further!");
            }


        }

        public void SellTurret()
        {
            turretLevel = 0;
            Destroy(currentTurret.gameObject);
        }

        public void SetNewTurret(Turret turret)
        {
            Destroy(currentTurret.gameObject);
            currentTurret = turret;
            turretLevel++;
        }

    }



}
