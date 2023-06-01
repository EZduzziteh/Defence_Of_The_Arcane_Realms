namespace Gameplay
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UI_SpellBook : MonoBehaviour
    {
        public GameObject spellBookUI;
        bool isSpellbookShown = false;
        Animator anim;

        AudioSource aud;

        [SerializeField]
        AudioClip openSpellbookSoundeffect;


        List<UI_Spell_Cast> spellButtons = new List<UI_Spell_Cast>();

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

        }
        private void Start()
        {
            anim = spellBookUI.GetComponent<Animator>();
            AudioManager audMan = FindObjectOfType<AudioManager>();
            audMan.managedSoundSources.Add(aud);
            aud.volume = audMan.soundEffectVolume;

           foreach(UI_Spell_Cast spell in FindObjectsOfType<UI_Spell_Cast>())
            {
                spellButtons.Add(spell);
            }
        }
        public void ToggleSpellBook()
        {
            aud.clip = openSpellbookSoundeffect;
            aud.Play();

            anim.SetBool("HasStarted", true);

            if (isSpellbookShown)
            {
                //spellBookUI.SetActive(false);
                isSpellbookShown = false;
                anim.SetBool("Displayed", false);
            }
            else
            {
                //spellBookUI.SetActive(true);
                isSpellbookShown = true;

                anim.SetBool("Displayed", true);
            }

        }

        internal void RefreshSpellUI()
        {
           foreach(UI_Spell_Cast spell in spellButtons)
            {
                spell.CheckCanCast();
            }
        }
    }
}
