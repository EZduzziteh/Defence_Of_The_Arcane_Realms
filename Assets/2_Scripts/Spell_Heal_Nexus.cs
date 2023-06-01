namespace Gameplay
{
    using UnityEngine;

    public class Spell_Heal_Nexus : UI_Spell_Cast
    {

        [Header("Heal Settings")]
        [SerializeField]
        float healAmount = 100.0f;



        protected override void HandleStartCastingSpell()
        {

            FindObjectOfType<Nexus>().GetComponent<Health>().HealDamage(healAmount);
        }





    }
}