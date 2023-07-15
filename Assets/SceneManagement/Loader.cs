using Player.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class Loader : MonoBehaviour
    {
        public enum Scene
        {
            MainMenu,
            Game,
            EndGame,
            LeaderBoard
        }
        
        
        
        public void MainMenu()
        {
            SceneManager.LoadScene(Scene.MainMenu.ToString());
        }
        public void Game()
        {
            SceneManager.LoadScene(Scene.Game.ToString());
        }
        
        public static void EndGame()
        {
            SceneManager.LoadScene(Scene.EndGame.ToString());
        }

        public void LeaderBoard()
        {
            SceneManager.LoadScene(Scene.LeaderBoard.ToString());
        }
        
        public void Quit()
        {
            Application.Quit();
        }

    }
}
