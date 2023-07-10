using UnityEngine;

namespace Player.Scripts
{
    [CreateAssetMenu(menuName = "Stats/Player", fileName = "New Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        [field: SerializeField] public float Health { get; set; }
        [field: SerializeField] public float AttackDamage { get; set; }
        [field: SerializeField] public float AttackSpeed { get; set; }
        [field: SerializeField] public float DashCooldown { get; set; }
        [field: SerializeField] public float AbilityCooldown { get; set; }
        [field: SerializeField] public float AbilityDamage { get; set; }
    }
}