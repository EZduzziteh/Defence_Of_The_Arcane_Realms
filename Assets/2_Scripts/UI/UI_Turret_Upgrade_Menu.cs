namespace Gameplay
{
    using System;
    using UnityEngine;

    public class UI_Turret_Upgrade_Menu : MonoBehaviour
    {
        UI_Turret_Upgrade upgradeButton;
        UI_Turret_Sell sellButton;


        private void Start()
        {
            if (!upgradeButton)
            {
                upgradeButton = GetComponentsInChildren<UI_Turret_Upgrade>(true)[0];
            }

            if (!sellButton)
            {
                sellButton = GetComponentsInChildren<UI_Turret_Sell>(true)[0];
            }
        }
        public void UpdatePrices(int upgradePrice, int sellPrice)
        {

            if (!upgradeButton)
            {
                upgradeButton = GetComponentsInChildren<UI_Turret_Upgrade>(true)[0];
            }

            if (!sellButton)
            {
                sellButton = GetComponentsInChildren<UI_Turret_Sell>(true)[0];
            }
            upgradeButton.SetUpgradeCost(upgradePrice);
            sellButton.SetSellPrice(sellPrice);
        }

        internal void RefreshUpgradeUI()
        {
            if (!upgradeButton)
            {
                upgradeButton = GetComponentsInChildren<UI_Turret_Upgrade>(true)[0];
            }

            if (!sellButton)
            {
                sellButton = GetComponentsInChildren<UI_Turret_Sell>(true)[0];
            }
            upgradeButton.CheckCanUpgrade();
            
        }
    }
}