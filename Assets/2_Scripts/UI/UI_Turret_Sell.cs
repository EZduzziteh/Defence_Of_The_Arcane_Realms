namespace Gameplay
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    public class UI_Turret_Sell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        PlayerController controller;
        [SerializeField]
        int sellPrice = 50;
        [SerializeField]
        TextMeshProUGUI sellText;
        [SerializeField]
        Image sellBackground;
        UI_Manager uiManager;

        private void Start()
        {
            controller = FindObjectOfType<PlayerController>();
            uiManager = FindObjectOfType<UI_Manager>();
        }



        public void OnClick()
        {


            if (controller.currentlySelectedTowerBase)
            {
                controller.AddGold(sellPrice);
                controller.currentlySelectedTowerBase.SellTurret();

                controller.currentlySelectedTowerBase = null;
                uiManager.CloseAllMenus();
            }
            else
            {
                Debug.Log("No Tower Base Selected!");
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {


            sellBackground.gameObject.SetActive(false);
            sellText.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            sellText.text = "+" + sellPrice.ToString();
            sellText.color = Color.green;
            sellText.gameObject.SetActive(true);

            sellBackground.gameObject.SetActive(true);

        }


  

     
        public void SetSellPrice(int amount)
        {
            sellPrice = amount;
        }
    }
}