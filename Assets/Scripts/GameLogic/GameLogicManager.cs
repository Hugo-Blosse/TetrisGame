using System;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicManager : MonoBehaviour
{
    public static event Action GameStart;
    public static event Action GameEnd;

    private int remainingPlayers;
    private TetrisManager tetrisManager1;
    private TetrisManager tetrisManager2;
    
    void Start()
    {
        tetrisManager1 = GameObject.FindGameObjectWithTag("Tetris1").GetComponent<TetrisManager>();
        tetrisManager2 = GameObject.FindGameObjectWithTag("Tetris2").GetComponent<TetrisManager>();
        StartGame();
    }

    // Initialize starting parameters
    public void StartGame()
    {
        remainingPlayers = 2;
        tetrisManager1.StartTetrisManager();
        tetrisManager2.StartTetrisManager();
    }
    // End game when both players cannot get more blocks
    public void PlayerLost()
    {
        remainingPlayers -= 1;
        if (remainingPlayers == 0)
        {
            GameEnd?.Invoke();
        }
    }
    
    // Delete remaining pieces - required when restarting the game
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
    // Start the game from beginning - when correct button is pressed and both players cannot continue playing current game
    public void OnRestart()
    {
        if (remainingPlayers <= 0)
        {
            DeletePieces();
            StartGame();
            GameStart?.Invoke();
        }
    }
}
