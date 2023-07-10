using UnityEngine;

namespace Enemies.Scripts
{
    [CreateAssetMenu(menuName = "Stats/Enemy", fileName = "New Enemy Stats")]
    public class EnemyStatsSO : ScriptableObject
    {
        [field: SerializeField] public float Health { get; set; }
        [field: SerializeField] public float AttackDamage { get; set; }
        [field: SerializeField] public float AttackSpeed { get; set; }
        [field: SerializeField] public float AttackRange { get; set; }
        [field: SerializeField] public float Speed { get; set; }
    
    }
}