namespace Gameplay
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class UI_Spell_Cast : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Spell Settings")]
        [SerializeField]
        protected string spellName;
        [SerializeField]
        protected int spellManaCost = 100;


        [Header("UI References")]
        [SerializeField]
        TextMeshProUGUI costText;
        [SerializeField]
        Image costBackground;

        protected PlayerController controller;
        AudioSource aud;
        [Header("Audio Settings")]
        [SerializeField]
        AudioClip spellCastSoundEffect;



        public Texture2D CastSpellCursorTexture;


        // Start is called before the first frame update
        void Start()
        {
            aud = GetComponent<AudioSource>();
            AudioManager audMan = FindObjectOfType<AudioManager>();
            audMan.managedSoundSources.Add(aud);
            aud.volume = audMan.soundEffectVolume;
            //#TODO subscribe to audiomanager onchanged event



            controller = FindObjectOfType<PlayerController>();

            CheckCanCast();
        }



        public void OnClick()
        {

            if (controller.TrySpendMana(spellManaCost))
            {
                if (spellCastSoundEffect)
                {
                    aud.clip = spellCastSoundEffect;
                    aud.Play();
                }

                if (CastSpellCursorTexture)
                {
                    FindObjectOfType<CursorManager>().SetCursor(CastSpellCursorTexture);
                }

                HandleStartCastingSpell();
                CheckCanCast();
            }


        }
        public void CheckCanCast()
        {
            if (controller.mana >= spellManaCost)
            {
                costBackground.color = Color.green;
            }
            else
            {
                costBackground.color = Color.red;
            }
        }


        protected virtual void HandleStartCastingSpell()
        {
            Debug.Log("cast Base");
        }

        //controller.isCastingFireball = true;
        public void OnPointerExit(PointerEventData eventData)
        {


            costText.text = spellName;
            CheckCanCast();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            costText.text = "-" + spellManaCost;
            CheckCanCast();



        }
    }
}