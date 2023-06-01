namespace Menu
{
    using UnityEngine;

    public class LevelButton : MonoBehaviour
    {
        public string levelToLoad = "Level_01";
        public GameObject markerLocation;
        [SerializeField]
        int starsEarned = 0;

        [SerializeField]
        Material goldMaterial;
        [SerializeField]
        Material blackMaterial;

        [SerializeField]
        GameObject star1;
        [SerializeField]
        GameObject star2;
        [SerializeField]
        GameObject star3;

        private void Start()
        {



            starsEarned = PlayerPrefs.GetInt("Stars_Earned_" + levelToLoad);



            star1.GetComponent<MeshRenderer>().material = blackMaterial;

            star2.GetComponent<MeshRenderer>().material = blackMaterial;

            star3.GetComponent<MeshRenderer>().material = blackMaterial;

            if (starsEarned > 0)
            {
                star1.GetComponent<MeshRenderer>().material = goldMaterial;
            }
            if (starsEarned > 1)
            {
                star2.GetComponent<MeshRenderer>().material = goldMaterial;
            }
            if (starsEarned > 2)
            {
                star3.GetComponent<MeshRenderer>().material = goldMaterial;
            }
        }

    }
}