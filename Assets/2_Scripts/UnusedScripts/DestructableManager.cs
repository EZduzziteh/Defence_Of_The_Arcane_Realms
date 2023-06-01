

namespace Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;
    public class DestructableManager : MonoBehaviour
    {
        public List<Destructable> destructables = new List<Destructable>();


        int destroyedIndex = 0;


        public void DestroyNext()
        {
            if (destroyedIndex < destructables.Count)
            {

                destructables[destroyedIndex].HandleDestruction();

                destroyedIndex++;
            }

        }



    }
}
