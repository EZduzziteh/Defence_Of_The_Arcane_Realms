namespace Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    public class FrostChanger : MonoBehaviour
    {
        List<Renderer> meshRenderers = new List<Renderer>();

        [Header("Frost Settings")]

        [SerializeField]
        [Range(0, 1)] protected float frostAmount = 0.0f;
        private float lastFrostAmount = 0.0f;
        Material frostMaterial;


        // Start is called before the first frame update
        void Start()
        {
            lastFrostAmount = frostAmount;

            //get a child objects renderer to create a new material instance
            frostMaterial = GetComponent<Renderer>().material;
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                meshRenderers.Add(renderer);
                //assign the same material instance to all of the renderers
                renderer.material = frostMaterial;
            }

            UpdateFrostMaterial();



        }

        private void Update()
        {

            //only update if the value was changed
            if (lastFrostAmount != frostAmount)
            {
                lastFrostAmount = frostAmount;
                UpdateFrostMaterial();
            }
        }

        public void setFrostAmount(float amount)
        {
            frostAmount = amount;
        }
        public void UpdateFrostMaterial()
        {

            frostMaterial.SetFloat("_FrostAmount", frostAmount);
        }

    }



}