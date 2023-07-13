using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities.Scripts
{
    [CreateAssetMenu(menuName = "Stats/Ability", fileName = "New Ability Stats")]
    public class AbilitySO : ScriptableObject
    {
        [field: SerializeField] public GameObject AbilityPrefab { get; private set; }
        [field: SerializeField] public float AbilityCooldown { get; set; }
        [field: SerializeField] public float AbilityDamage { get; set; }
        [field: SerializeField] public float Time2Live { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

    }
}