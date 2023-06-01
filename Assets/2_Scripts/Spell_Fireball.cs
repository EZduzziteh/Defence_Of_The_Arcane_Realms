namespace Gameplay
{
    using UnityEngine;

    public class Spell_Fireball : UI_Spell_Cast
    {
        protected override void HandleStartCastingSpell()
        {
            controller.isCastingFireball = true;
            Debug.Log("fireballing");
        }

    }
}