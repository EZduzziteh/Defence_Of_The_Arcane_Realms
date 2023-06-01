namespace Gameplay
{
    using System;
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class UI_Turret_Build_Menu : MonoBehaviour
    {

        List<UI_Turret_Build> buildButtons = new List<UI_Turret_Build>();
        internal void RefreshBuildUI()
        {
           foreach(UI_Turret_Build button in buildButtons)
            {
                button.CheckCanBuild();
            }
        }
    }
}