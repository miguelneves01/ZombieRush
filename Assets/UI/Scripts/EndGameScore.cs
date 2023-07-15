using System.Collections;
using System.Collections.Generic;
using Player.Scripts;
using TMPro;
using UnityEngine;

public class EndGameScore : MonoBehaviour
{
    [SerializeField] private PlayerScore _playerScore;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    
    // Start is called before the first frame update
    void Awake()
    {
        _scoreText.text = _playerScore.CurrentScore.ToString("000000");
        _highScoreText.text = _playerScore.HighScore.ToString("000000");
    }
}
