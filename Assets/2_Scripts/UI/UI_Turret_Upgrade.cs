namespace Gameplay
{
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class UI_Turret_Upgrade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        PlayerController controller;
        [SerializeField]
        int upgradePrice = 100;
        [SerializeField]
        TextMeshProUGUI costText;
        [SerializeField]
        Image costBackground;

        UI_Manager uiManager;

        private void Start()
        {
            controller = FindObjectOfType<PlayerController>();
            costText.text = upgradePrice.ToString();
            uiManager = FindObjectOfType<UI_Manager>();
        }



        public void OnClick()
        {

            if (controller.gold >= upgradePrice)
            {
                if (controller.currentlySelectedTowerBase)
                {
                    if (controller.TrySpendGold(upgradePrice))
                    {

                        controller.currentlySelectedTowerBase.UpgradeTurret();
                        controller.currentlySelectedTowerBase = null;

                        uiManager.CloseAllMenus();
                    }
                    else
                    {
                        Debug.Log("not enough gold");
                    }
                }
                else
                {
                    Debug.Log("No Tower Base Selected!");
                }
            }
            else
            {
                Debug.Log("Not enough gold to build turret!");
            }
        }

        public void CheckCanUpgrade()
        {

            if (controller.gold >= upgradePrice)
            {
                costText.color = Color.green;
            }
            else
            {
                costText.color = Color.red;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {


            costBackground.gameObject.SetActive(false);
            costText.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            costText.text = "-" + upgradePrice.ToString();
            costText.gameObject.SetActive(true);

            costBackground.gameObject.SetActive(true);
            CheckCanUpgrade();

        }

        public void SetUpgradeCost(int amount)
        {
            upgradePrice = amount;
        }
    }
}