using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private TetrisManager tetrisManager1;
    private TetrisManager tetrisManager2;
    private UIManager uiManager1;
    private UIManager uiManager2;
    private int remainingPlayers;
    private Text endScreen;
    void Start()
    {
        tetrisManager1 = GameObject.FindGameObjectWithTag("Tetris1").GetComponent<TetrisManager>();
        tetrisManager2 = GameObject.FindGameObjectWithTag("Tetris2").GetComponent<TetrisManager>();
        uiManager1 = GameObject.FindGameObjectWithTag("UI1").GetComponent<UIManager>();
        uiManager2 = GameObject.FindGameObjectWithTag("UI2").GetComponent<UIManager>();
        endScreen = GameObject.FindGameObjectWithTag("EndScreen").GetComponent<Text>();
        StartGame();
    }

    // Update is called once per frame
    public void StartGame()
    {
        uiManager1.StartUIManager();
        uiManager2.StartUIManager();
        tetrisManager1.StartTetrisManager();
        tetrisManager2.StartTetrisManager();
        remainingPlayers = 2;
        endScreen.text = "";
    }
    void Update()
    {
        
    }
    public void PlayerLost()
    {
        remainingPlayers -= 1;
        if (remainingPlayers == 0)
        {
            GameEnd();
        }
    }
    void GameEnd()
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
    void DeletePieces()
    {
        Piece[] pieces1 = tetrisManager1.GetComponentsInChildren<Piece>();
        Piece[] pieces2 = tetrisManager2.GetComponentsInChildren<Piece>();
        foreach (Piece piece in pieces1)
        {
            Destroy(piece.gameObject);
        }
        foreach (Piece piece in pieces2)
        {
            Destroy(piece.gameObject);
        }
    }
    public void OnRestart()
    {
        if (remainingPlayers == 0)
        {
            DeletePieces();
            StartGame();
        }
    }
}
