using UnityEngine;
using UnityEngine.UI;

public class ManaConsumptionBar : MonoBehaviour
{

    [SerializeField]
    RectTransform manaBar;
    [SerializeField]
    Slider manaSlider;
    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();

        rect.offsetMin = new Vector2(-204.809f, rect.offsetMin.y);

        rect.offsetMax = new Vector2(-150.131f, rect.offsetMax.y);


    }


    /*     public static void SetLeft(this RectTransform rt, float left)
     {
         rt.offsetMin = new Vector2(left, rt.offsetMin.y);
     }
    */

    // Update is called once per frame
    void Update()
    {

    }
}
