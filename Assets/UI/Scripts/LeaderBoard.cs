using System;
using System.Collections.Generic;
using Dan.Main;
using TMPro;
using UnityEngine;

namespace UI.Scripts
{
    public class LeaderBoard : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> _names;
        [SerializeField] private List<TextMeshProUGUI> _scores;
    
        private const string LeaderBoardKey = "60fc78c5353ae6c9e0b9e5106deb24e716652c298c1eb0b4989d1a7f61c5c654";

        private void Start()
        {
            GetLeaderboard();
        }

        public void GetLeaderboard()
        {
            LeaderboardCreator.GetLeaderboard(LeaderBoardKey, ((msg) =>
            {
                int length = _names.Count > msg.Length ? msg.Length : _names.Count;
                for (int i = 0; i < length; i++)
                {
                    _names[i].text = msg[i].Username;
                    _scores[i].text = msg[i].Score.ToString("000000");
                } 
            }));
        }

        public void SetLeaderBoardEntry(string username, int score)
        {
            LeaderboardCreator.UploadNewEntry(LeaderBoardKey, username, score,
                ((msg) =>
                {
                    GetLeaderboard();
                }));
        }
    }
}
