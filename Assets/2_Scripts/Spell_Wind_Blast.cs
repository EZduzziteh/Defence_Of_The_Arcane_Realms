namespace Gameplay
{

    public class Spell_Wind_Blast : UI_Spell_Cast
    {
        protected override void HandleStartCastingSpell()
        {
            controller.isCastingWindBlast = true;
        }
    }
}