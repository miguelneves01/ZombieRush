using System;
using UnityEngine;

namespace UI.Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        private bool _isPaused;
        [SerializeField] private Transform _pauseMenu;

        private void Start()
        {
            Time.timeScale = 1f;
        }

        void Update()
        {
             if (Input.GetKeyDown(KeyCode.Escape))
             {
                 _isPaused = !_isPaused;
                 
                 if (_isPaused)
                 {
                     Time.timeScale = 0f;
                 }
                 else
                 {
                     Time.timeScale = 1f;
                 }
             }


            
             _pauseMenu.gameObject.SetActive(_isPaused);
        }

        public void Resume()
        {
            _isPaused = false;
            Time.timeScale = 1f;
        }
    }
}