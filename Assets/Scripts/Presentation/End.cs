using UnityEngine;
using UnityEngine.UI;

namespace Presentation
{
    public class End : MonoBehaviour
    {
        private UIManager uiManager1;
        private UIManager uiManager2;
        private Text endScreen;
        private void Start()
        {
            uiManager1 = GameObject.FindGameObjectWithTag("UI1").GetComponent<UIManager>();
            uiManager2 = GameObject.FindGameObjectWithTag("UI2").GetComponent<UIManager>();
            endScreen = GetComponent<Text>();
        }
        // Display a winner or draw message
        void OnGameEnd()
        {
            int scoreP1 = uiManager1.GetScore();
            int scoreP2 = uiManager2.GetScore();
            if (scoreP1 > scoreP2)
            {
                endScreen.text = "Player 1 wins!";
            }
            else if (scoreP1 < scoreP2)
            {
                endScreen.text = "Player 2 wins!";
            }
            else
            {
                endScreen.text = "Draw!";
            }
            endScreen.text += "\nPress Enter or Start to play again.";
        }
        public void OnEnable()
        {
            GameLogicManager.GameEnd += OnGameEnd;
        }
    }
}
