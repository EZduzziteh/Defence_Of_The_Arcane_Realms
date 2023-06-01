

namespace Menu
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class LevelLoader : MonoBehaviour
    {

        public void OpenLevel(string levelToLoad)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(levelToLoad);
        }

        public void ToggleObject(GameObject obj)
        {
            if (!obj)
            {
                return;
            }
            if (obj.activeInHierarchy)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }

        bool isOptionsDisplayed = false;
        public void ToggleOptions(GameObject obj)
        {
            Animator anim = obj.GetComponent<Animator>();


            anim.SetBool("Initial", true);

            if (isOptionsDisplayed)
            {
                anim.SetBool("Displayed", false);
                isOptionsDisplayed = false;
            }
            else
            {
                anim.SetBool("Displayed", true);
                isOptionsDisplayed = true;

            }

        }

        public void ReloadLevel()
        {
            Time.timeScale = 1;
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}