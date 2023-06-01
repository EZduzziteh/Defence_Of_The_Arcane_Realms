namespace Gameplay
{
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "EnemyData", menuName = "MyGame/Enemy Data")]
    public class Enemy_Data : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Basic Info")]
        [LabelWidth(100)]
        [PreviewField(100)]
        GameObject enemyPrefab;

        [TextArea]
        [SerializeField]
        string description;

        [VerticalGroup("Basic Info/Stats")]
        [SerializeField]
        float attackRate;

        [VerticalGroup("Basic Info/Stats")]
        [SerializeField]
        float attackDamage;




        //#TODO Refactor this into a separate "stats" class? scriptable object amaybe
        [Header("Combat Settings")]

        [VerticalGroup("Basic Info/Stats")]
        [SerializeField]
        protected float timeBetweenAttacks = 1.5f;
        [SerializeField]
        [VerticalGroup("Basic Info/Stats")]
        protected float damage = 100;
        [VerticalGroup("Basic Info/Stats")]
        [SerializeField]
        protected float attackDoDamageDelay = 0.5f;


        [SerializeField]
        protected float deathSinkRate = 1.0f;
        [SerializeField]
        protected float timeBeforeDeathSink = 2.0f;

        [Header("Reward Settings")]
        [SerializeField]
        protected int goldValue = 1;

        [Header("Speed Settings")]
        [SerializeField]
        protected float baseSpeed = 2.0f;

        [Header("Sound Settings")]
        [SerializeField]
        protected List<AudioClip> deathSoundEffects = new List<AudioClip>();









    }
}
