

namespace Gameplay
{
    using UnityEngine;

    public class CursorManager : MonoBehaviour
    {
        [Header("Cursor Settings")]
        [SerializeField]
        Texture2D defaultCursor;

        public void Start()
        {
            SetDefaultCursor();
        }

        public void SetCursor(Texture2D cursorTexture)
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        public void SetDefaultCursor()
        {

            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
