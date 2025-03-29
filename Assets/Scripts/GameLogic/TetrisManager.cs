using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    public static event Action<int, char> SetPreviewImages;
    public static event Action<int, char> AddScore;
    public GameObject[] Tetromines;

    private int TetrominoIndex;
    private float moveVal;
    private float speedUpVal;
    private bool canCreateTetromino = false;
    private GameObject currentTetromino;
    private Piece[] currentPieces;
    private RaycastHit2D[] hits;
    
    // Spawn first tetromino block and initiate preview
    public void StartTetrisManager()
    {
        TetrominoIndex = GetNextTetrominoIndex();
        SpawnTetromino();
        PreviewNextTetromino();
    }
    void Update()
    {
        // Change speed of current pieces
        if (currentPieces != null)
        {
            for (int i = 0; i < currentPieces.Length; i++)
            {
                if (currentPieces[i].IsCurrentTetromino)
                {
                    currentPieces[i].SetSpeed(moveVal, speedUpVal);
                }
            }
        }
    }
    // Set X speed of current pieces when player is moving
    public void OnMove(float moveVal)
    {
        this.moveVal = moveVal;
    }
    // Speed up tetromino pieces falling
    public void OnSpeedUp(float speedUpVal)
    {
        this.speedUpVal = speedUpVal;
    }
    public void CheckForLines(bool IsNotCurrentTetromino)
    {
        if (canCreateTetromino && IsNotCurrentTetromino)
        {
            // Check if tetromino spawning position is avalible
            hits = Physics2D.LinecastAll(new Vector2(-1.5f + transform.position.x, 17f), new Vector2(1.5f + transform.position.x, 17f), 1);
            if (hits.Length == 0)
            {
                // Create next tetromino and set tetromino preview when spawn is avalible
                SpawnTetromino();
                PreviewNextTetromino();
            }
            else
            {
                // End game for this player
                FindAnyObjectByType<GameLogicManager>().GetComponent<GameLogicManager>().PlayerLost();
                return;
            }
        }
        int score = 0;
        // Check for tetromino block lines
        for (int i = -16; i < 17; i++) {
            hits = Physics2D.LinecastAll(new Vector2(-8f + transform.position.x, -i*1f), new Vector2(8f + transform.position.x, -i*1f), 1);
            // Update score and destroy tetromino pieces when there are 10 or more tetromino pieces in line
            if (hits.Length >= 10)
            {
                score += 1 * (score + 1);
                foreach (RaycastHit2D hit in hits)
                {
                    Destroy(hit.rigidbody.gameObject);
                }
            }
        }
        // If tetromino line was detected and destroyed tetromino pieces start falling again (fallen tetrominoes can create more lines)
        if (score > 0)
        {
            Piece[] allPieces = gameObject.GetComponentsInChildren<Piece>();
            foreach (Piece piece in allPieces)
            {
                piece.StartFalling();
            }
        }
        // Add score for first time tetromino collision
        if (IsNotCurrentTetromino)
        {
            score = 1 + (score * 100);
        }
        else
        {
            score *= 100;
        }
        // Set new score in correct UIManager
        AddScore?.Invoke(score, tag[^1]);
    }
    int GetNextTetrominoIndex()
    {
        return UnityEngine.Random.Range(0, Tetromines.Length);
    }
    // Set tetromino index for next tetromino spawn
    // Set tetromino preview in correct UIManager
    void PreviewNextTetromino()
    {
        TetrominoIndex = GetNextTetrominoIndex();
        SetPreviewImages?.Invoke(TetrominoIndex, tag[^1]);   
    }
    // Create new tetromino block
    // Set current tetromino pieces
    // Disable tetromino spawning
    public void SpawnTetromino()
    {   
        currentTetromino = Instantiate(Tetromines[TetrominoIndex], transform.position + new Vector3(0,17,0), Quaternion.identity);
        currentTetromino.transform.parent = transform;
        currentPieces = currentTetromino.GetComponentsInChildren<Piece>();
        foreach (Piece piece in currentPieces)
        {
            piece.SetTetrisManager(this);
        }
        canCreateTetromino = false;
    }
    // Enable tetromino spawning
    public void SetCanCreateTetromino()
    {
        canCreateTetromino = true;
    }
    public void OnEnable()
    {
        GameLogicManager.GameStart += StartTetrisManager;
    }
}
