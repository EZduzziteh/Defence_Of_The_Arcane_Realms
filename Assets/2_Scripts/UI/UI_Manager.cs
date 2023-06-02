namespace Gameplay
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UI_Manager : MonoBehaviour
    {

        UI_Ready_Button readyButton;

        UI_Turret_Build_Menu turretBuildMenu;
        UI_Turret_Upgrade_Menu turretUpgradeMenu;
        UI_Pause_Menu pauseMenu;
        UI_Win_Screen winScreen;
        UI_Game_Over gameOver;
        PlayerController controller;

        [SerializeField]
        TMPro.TextMeshProUGUI goldDisplayText;
        [SerializeField]
        Slider manaBar;

        UI_SpellBook spellbook;


        private void Start()
        {
            turretBuildMenu = FindObjectOfType<UI_Turret_Build_Menu>(true);
            turretUpgradeMenu = FindObjectOfType<UI_Turret_Upgrade_Menu>(true);
            readyButton = FindObjectOfType<UI_Ready_Button>();
            pauseMenu = FindObjectOfType<UI_Pause_Menu>(true);
            winScreen = FindObjectOfType<UI_Win_Screen>(true);
            gameOver = FindObjectOfType<UI_Game_Over>(true);
            controller = FindObjectOfType<PlayerController>(true);
            spellbook = FindObjectOfType<UI_SpellBook>();


        }

        public void SetGoldTextValue(int value)
        {
            
            goldDisplayText.text = value + " gold";
        }
        public void ReadyToStartLevel()
        {
            FindObjectOfType<WaveManager>().readyToStart = true;
            readyButton.gameObject.SetActive(false);
        }

        public void ShowBuildTurretMenu()
        {

            turretBuildMenu.gameObject.SetActive(true);
        }

        public void ShowTurretUpgradeMenu()
        {
            CloseAllMenus();
            turretUpgradeMenu.gameObject.SetActive(true);
        }

        internal void HandleGameOver()
        {
            gameOver.gameObject.SetActive(true);
        }

        public void HandleLevelComplete()
        {
            winScreen.gameObject.SetActive(true);

        }

        public void CloseAllMenus()
        {

            turretUpgradeMenu.gameObject.SetActive(false);
            controller.TurnOffIndicator();
            turretBuildMenu.gameObject.SetActive(false);
        }

        public void SetCurrentMana(float value)
        {
            manaBar.value = value;
            spellbook.RefreshSpellUI();

        }

        public void SetMaxMana(float value)
        {
            manaBar.maxValue = value;
        }

        public void UpdateTurretUpgradeMenu(Turret currentTurret)
        {
            float sellPercentage = 0.5f; //set this to change what the sellvalue is, 0.5 means it sells for half of the original cost.


            if (currentTurret.GetUpgradeTurret())
            {
                turretUpgradeMenu.UpdatePrices(currentTurret.GetUpgradeTurret().getTurretPrice(), Mathf.RoundToInt(currentTurret.getTurretPrice() * sellPercentage));
            }
            else
            {
                
            }
        }

        public void TogglePauseMenu()
        {
            if (pauseMenu.isActiveAndEnabled)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1.0f;

            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }
}