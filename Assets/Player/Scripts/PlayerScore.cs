using UnityEngine;

namespace Player.Scripts
{
    [CreateAssetMenu(menuName = "PlayerScore",fileName = "PlayerScore")]
    public class PlayerScore : ScriptableObject
    {
        [field: SerializeField] public int HighScore { get; set; }
        [field: SerializeField] public int CurrentScore { get; set; }
    }
}