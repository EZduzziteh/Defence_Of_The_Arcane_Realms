namespace Gameplay
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class UI_Turret_Sell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        PlayerController controller;
        [SerializeField]
        int sellPrice = 50;
        [SerializeField]
        TextMeshProUGUI sellText;

        UI_Manager uiManager;

        private void Start()
        {
            controller = FindObjectOfType<PlayerController>();
            sellText.text = sellPrice.ToString();
            uiManager = FindObjectOfType<UI_Manager>();
        }



        public void OnClick()
        {


            if (controller.currentlySelectedTowerBase)
            {
                controller.AddGold(sellPrice);
                controller.currentlySelectedTowerBase.SellTurret();
                uiManager.CloseAllMenus();
            }
            else
            {
                Debug.Log("No Tower Base Selected!");
            }

        }


        public void OnPointerExit(PointerEventData eventData)
        {


            sellText.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            sellText.text = "+" + sellPrice.ToString();
            sellText.gameObject.SetActive(true);


            sellText.color = Color.green;



        }
        public void SetSellPrice(int amount)
        {
            sellPrice = amount;
        }
    }
}