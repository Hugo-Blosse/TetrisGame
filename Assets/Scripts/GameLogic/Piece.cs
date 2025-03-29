using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Piece : MonoBehaviour
{
    public bool IsCurrentTetromino;

    private float yspeed;
    private float xspeed;
    private float time;
    private float lastYPosition;
    private Rigidbody2D rigidBody;
    private TetrisManager tetrisManager;
    
    // Set starting parameters
    void Start()
    {
        time = 0;
        yspeed = 2;
        xspeed = 0;
        IsCurrentTetromino = true;
        rigidBody = GetComponent<Rigidbody2D>();
        // Enable next tetromino spawn
        tetrisManager.SetCanCreateTetromino();
        rigidBody.useFullKinematicContacts = true;
    }
    void Update()
    {
        // Set tetromino piece velocity
        rigidBody.linearVelocity = new Vector2(1f * xspeed, -1f * yspeed);
        // Set tetromino velocity to zero, when it stops falling
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                // Check if y position difference is below 0.3
                float currYPosition = transform.position.y;
                if (Mathf.Abs(currYPosition - lastYPosition) < 0.3)
                {
                    foreach (Piece child in rigidBody.transform.parent.GetComponentsInChildren<Piece>())
                    {
                        child.SetLinearVelocityToZero();
                    }
                }
                else
                {
                    time = 1;
                }
            }

        }
    }
    // Handle tetromino piece collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if tetromino is colliding with itself or wall - should not stop
        Piece pieceHit = collision.gameObject.GetComponent<Piece>();
        bool isNotHittingItselfOrWall = false;
        if (pieceHit && !pieceHit.IsCurrentTetromino)
        {
            isNotHittingItselfOrWall = true;
        }
        // On floor and other tetromino piece collision
        // Check for line after collision
        if (collision.gameObject.CompareTag("Floor") || isNotHittingItselfOrWall)
        {
            // Tetromino controller by player stops moving and being controlled by player
            if (IsCurrentTetromino) 
            {
                foreach (Piece child in rigidBody.transform.parent.GetComponentsInChildren<Piece>())
                {
                    child.SetLinearVelocityToZero();
                }
                tetrisManager.CheckForLines(IsCurrentTetromino);
                foreach (Piece child in rigidBody.transform.parent.GetComponentsInChildren<Piece>())
                {
                    child.IsCurrentTetromino = false;
                }
            }
            else
            {
                tetrisManager.CheckForLines(IsCurrentTetromino);
            }
        }   
    }
    public void SetTetrisManager(TetrisManager tetrisManager)
    {
        this.tetrisManager = tetrisManager;
    }
    // Stop tetromino movement
    void SetLinearVelocityToZero()
    {
        xspeed = 0;
        yspeed = 0;
        rigidBody.Sleep();
    }
    // Start tetromino movement after detecting line
    public void StartFalling()
    {
        yspeed = 2;
        time = 1;
        lastYPosition = transform.position.y;
        if (rigidBody is not null)
        {
            rigidBody.WakeUp();
        }
    }
    public void SetSpeed(float velocityX, float velocityY)
    {
        xspeed = 5f * velocityX;
        yspeed = 2f + 5f*velocityY;
    }
}
