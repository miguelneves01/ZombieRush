using UnityEngine;

namespace Player.Scripts
{
    [CreateAssetMenu(menuName = "Stats/Player", fileName = "New Player Stats")]
    public class PlayerStatsSO : ScriptableObject
    {
        [field: SerializeField] public float InitialHealth { get; set; }
        [field: SerializeField] public float MaxHealth { get; set; }
        [field: SerializeField] public float Health { get; set; }
        [field: SerializeField] public float HealthRegen { get; set; }
        [field: SerializeField] public float AttackDamage { get; set; }
        [field: SerializeField] public float AttackSpeed { get; set; }
        [field: SerializeField] public float DashCooldown { get; set; }
        [field: SerializeField] public float DashPower { get; set; }
        [field: SerializeField] public float Speed { get; set; }
    
    }
}