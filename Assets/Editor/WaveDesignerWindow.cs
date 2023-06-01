using UnityEditor;
using UnityEngine;

public class WaveDesignerWindow : EditorWindow
{


    [MenuItem("Window/WaveDesigner")]
    static void OpenWindow()
    {
        WaveDesignerWindow window = (WaveDesignerWindow)GetWindow(typeof(WaveDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    [MenuItem("Example/FindAssets Example")]
    static void GetEnemies()
    {
        string[] guids1 = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/0_Prefabs" });

        foreach (string guid1 in guids1)
        {
            Debug.Log(AssetDatabase.GUIDToAssetPath(guid1));
        }


    }
}
