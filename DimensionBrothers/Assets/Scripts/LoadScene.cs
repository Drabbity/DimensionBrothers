using UnityEngine;

namespace DimensionBrothers
{
    public class LoadScene : MonoBehaviour
    {
        public void LoadNextLevel()
        {
            GameManager.Instance.LoadNextLevel();
        }

        public void LoadMenu()
        {
            GameManager.Instance.LoadMenu();
        }
    }
}