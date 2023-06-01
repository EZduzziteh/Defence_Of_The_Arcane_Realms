using UnityEngine;

public class FloatingText : MonoBehaviour
{

    [SerializeField]
    float DestroyTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(0, 2.5f, 0);
        Destroy(this.gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
