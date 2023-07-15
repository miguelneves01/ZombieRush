using System;
using Player.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Scripts
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private PlayerScore _playerScore;
        [SerializeField] private TextMeshProUGUI _scoreDisplay;
        [SerializeField] private TMP_InputField _input;

        public UnityEvent<string, int> SubmitScoreEvent;
        public void SubmitScore()
        {
            SubmitScoreEvent.Invoke(_input.text, _playerScore.CurrentScore);
        }

        private void Start()
        {
            _scoreDisplay.text = _playerScore.CurrentScore.ToString("000000");
        }
    }
}
