using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TetrisManager : MonoBehaviour
{
    public GameObject[] Tetromines;
    private GameObject currentTetromino;
    private Piece[] currentPieces;
    private RaycastHit2D[] hits;
    private bool canCreateTetromino = false;
    private int TetrominoIndex;
    private int moveVal;
    private int speedUpVal;
    private UIManager uiManager;
    public void StartTetrisManager()
    {
        TetrominoIndex = GetNextTetrominoIndex();
        uiManager = GameObject.FindGameObjectWithTag("UI" + tag[^1]).GetComponent<UIManager>();
        SpawnTetromino();
        PreviewNextTetromino();
    }
    void Update()
    {
        if (currentPieces != null)
        {
            for (int i = 0; i < currentPieces.Length; i++)
            {
                if (!currentPieces[i].IsStopped)
                {
                    currentPieces[i].SetSpeed(moveVal, speedUpVal);
                }
            }
        }
    }
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        moveVal = (int)callbackContext.ReadValue<float>();
    }
    public void OnSpeedUp(InputAction.CallbackContext callbackContext)
    {
        speedUpVal = (int)callbackContext.ReadValue<float>();
    }
    public void Check(bool IsNotCurrentTetromino)
    {
        int score = 0;
        for (int i = -16; i < 17 ;i++) {
            hits = Physics2D.LinecastAll(new Vector2(-8f + transform.position.x, -i*1f), new Vector2(8f + transform.position.x, -i*1f), 1);
            if (hits.Length >= 10)
            {
                score += 1 * (score + 1);
                foreach (RaycastHit2D hit in hits)
                {
                    Destroy(hit.rigidbody.gameObject);
                }
            }
        }
        if (score > 0)
        {
            Piece[] allPieces = gameObject.GetComponentsInChildren<Piece>();
            foreach (Piece piece in allPieces)
            {
                piece.StartFalling();
            }
        }
        if (IsNotCurrentTetromino)
        {
            score = 1 + (score * 100);
        }
        else
        {
            score *= 100;
        }
        uiManager.AddScore(score);
        
        if (canCreateTetromino && IsNotCurrentTetromino)
        {
            hits = Physics2D.LinecastAll(new Vector2(-8f + transform.position.x, 17f), new Vector2(8f + transform.position.x, 17f), 1);
            if (hits.Length == 0)
            {
                SpawnTetromino();
                PreviewNextTetromino();
            }
            else
            {
                FindAnyObjectByType<GameManager>().GetComponent<GameManager>().PlayerLost();
            }
        }
    }
    int GetNextTetrominoIndex()
    {
        return Random.Range(0, Tetromines.Length);
    }
    void PreviewNextTetromino()
    {
        TetrominoIndex = GetNextTetrominoIndex();
        uiManager.SetImages(TetrominoIndex);
    }
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
    public void SetCanCreateTetromino()
    {
        canCreateTetromino = true;
    }
}
