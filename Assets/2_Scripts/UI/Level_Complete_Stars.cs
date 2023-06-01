namespace Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Level_Complete_Stars : MonoBehaviour
    {
        public GameObject star_1;
        public GameObject star_2;
        public GameObject star_3;

        void Start()
        {
            Health nexus = FindObjectOfType<Nexus>().GetComponent<Health>();
            if (nexus.GetCurrentHealth() == nexus.GetMaxHealth())
            {
                StartCoroutine(Handle3Star());
            }
            else if (nexus.GetCurrentHealth() >= (nexus.GetMaxHealth() / 2))
            {
                StartCoroutine(Handle2Star());
            }
            else
            {
                StartCoroutine(Handle1Star());
            }


            

        }


        //ienumerators
        IEnumerator Handle1Star()
        {

            yield return new WaitForSeconds(0.5f);

            star_1.SetActive(true);

        }
        IEnumerator Handle2Star()
        {

            yield return new WaitForSeconds(0.5f);

            star_1.SetActive(true); 
            
            yield return new WaitForSeconds(0.5f);

            star_2.SetActive(true);

        }
        IEnumerator Handle3Star()
        {

            yield return new WaitForSeconds(0.5f);

            star_1.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            star_2.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            star_3.SetActive(true);

        }
    }
}