using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{

    public class Health : MonoBehaviour
    {

        float currentHealth;
        [SerializeField]
        float maxHealth;

        [SerializeField]
        Slider healthBar;

        public void Start()
        {
            currentHealth = maxHealth;
            if (healthBar)
            {
                healthBar.maxValue = maxHealth;
                healthBar.value = currentHealth;
            }

        }

        public void Update()
        {
            if (healthBar)
            {
                healthBar.transform.LookAt(Camera.main.transform);
            }
        }
        public void TakeDamage(float amount)
        {

            currentHealth -= amount;

            if (healthBar)
            {
                healthBar.value = currentHealth;
            }


            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }


        }

        private void Die()
        {
            if (GetComponent<Nexus>())
            {
                GetComponent<Nexus>().DestroyNexus();
            }
            else if (GetComponent<Enemy_AI>())
            {

                GetComponent<Enemy_AI>().HandleDeath();
            }
            else if (GetComponent<Destructable>())
            {
                GetComponent<Destructable>().HandleDestruction();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void HealDamage(float amount)
        {
            currentHealth += amount;

            if (healthBar)
            {
                healthBar.value = currentHealth;
            }

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        public float GetCurrentHealth()
        {
            return currentHealth;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        internal void HideHealthBar()
        {
            healthBar.gameObject.SetActive(false);
        }
    }
}