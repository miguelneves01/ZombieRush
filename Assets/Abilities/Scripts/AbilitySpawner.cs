using System;
using Interfaces;
using UnityEngine;

namespace Abilities.Scripts
{
    public class AbilitySpawner : MonoBehaviour
    {
        [field: SerializeField] public AbilitySO AbilityStats { get; set; }

        // Start is called before the first frame update
        public void FireAbility()
        {
            var spawnPos = new Vector3(transform.position.x + transform.localScale.x, transform.position.y, 0);
            Instantiate(AbilityStats.AbilityPrefab, spawnPos, Quaternion.identity);
        }
    }
}
