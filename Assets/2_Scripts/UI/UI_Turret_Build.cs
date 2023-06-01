namespace Gameplay
{
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class UI_Turret_Build : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public Turret turretPrefab;
        public int turretPrice = 100;

        [SerializeField]
        TextMeshProUGUI costText;
        [SerializeField]
        Image costBackground;

        PlayerController controller;
        UI_Manager uiManager;


        private void Awake()
        {

            controller = FindObjectOfType<PlayerController>();
            uiManager = FindObjectOfType<UI_Manager>();
        }
        // Start is called before the first frame update
        void Start()
        {

            turretPrice = turretPrefab.getTurretPrice();
            costText.text = turretPrice.ToString();

        }



        public void OnClick()
        {

            if (controller.gold >= turretPrice)
            {
                if (controller.currentlySelectedTowerBase)
                {
                    controller.TrySpendGold(turretPrice);
                    controller.currentlySelectedTowerBase.BuildTurret(turretPrefab);
                    controller.currentlySelectedTowerBase.currentTurret.HideTurretRange();
                    controller.currentlySelectedTowerBase = null;

                    uiManager.CloseAllMenus();
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

        public void OnPointerExit(PointerEventData eventData)
        {


            costBackground.gameObject.SetActive(false);
            costText.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            costText.text = "-" + turretPrice.ToString();
            costText.gameObject.SetActive(true);

            costBackground.gameObject.SetActive(true);

            CheckCanBuild();

          

        }

        public void CheckCanBuild()
        {
            if (controller.gold >= turretPrice)
            {
                costText.color = Color.green;
            }
            else
            {
                costText.color = Color.red;
            }
        }
    }
}