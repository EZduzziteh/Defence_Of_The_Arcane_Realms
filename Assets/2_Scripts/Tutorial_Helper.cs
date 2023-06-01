using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Tutorial_Helper : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI tutorialText;

    public List<string> tutorialMessages = new List<string>();

    int tutorialIndex = 0;
    bool isTutorialShowing = true;

    [SerializeField]
    GameObject tutorialPanel;

    public void AdvanceTutorialStep()
    {
        tutorialIndex++;
        if (tutorialIndex >= tutorialMessages.Count)
        {
            tutorialIndex = tutorialMessages.Count - 1;
        }
        tutorialText.text = tutorialMessages[tutorialIndex];
    }

    public void PreviousTutorialStep()
    {

        tutorialIndex--;
        if (tutorialIndex < 0)
        {
            tutorialIndex = 0;
        }
        tutorialText.text = tutorialMessages[tutorialIndex];
    }

    public void ToggleTutorialPanel()
    {
        if (isTutorialShowing)
        {

            isTutorialShowing = false;
            tutorialPanel.SetActive(false);

        }
        else
        {

            isTutorialShowing = true;   
            tutorialPanel.SetActive(true);
        }
    }
}
