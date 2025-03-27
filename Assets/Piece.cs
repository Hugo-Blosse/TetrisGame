using Unity.VisualScripting;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float yspeed;
    private float xspeed;
    public bool IsStopped = false;
    public bool IsCurrentTetromino = true;
    public string tetrisManagerTag;
    private TetrisManager tetrisManager;
    private float time = 0;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        yspeed = 2;
        tetrisManager.SetCanCreateTetromino();
        rigidBody.useFullKinematicContacts = true;
    }
    void Update()
    {
        rigidBody.linearVelocity = new Vector2(1f * xspeed, -1f * yspeed);
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                SetLinearVelocityToZero();
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Piece pieceHit = collision.gameObject.GetComponent<Piece>();
        bool isNotHittingItselfOrWall = false;
        if (pieceHit && !pieceHit.IsCurrentTetromino)
        {
            isNotHittingItselfOrWall = true;
        }
        if (collision.gameObject.CompareTag("Floor") || isNotHittingItselfOrWall)
        {
            if (!IsStopped) 
            {
                foreach (Piece child in rigidBody.transform.parent.GetComponentsInChildren<Piece>())
                {
                    child.SetLinearVelocityToZero();
                }
                tetrisManager.Check(IsCurrentTetromino);
                foreach (Piece child in rigidBody.transform.parent.GetComponentsInChildren<Piece>())
                {
                    child.IsCurrentTetromino = false;
                }
            }
            else
            {
                tetrisManager.Check(IsCurrentTetromino);
            }
        }   
    }
    public void SetTetrisManager(TetrisManager tetrisManager)
    {
        this.tetrisManager = tetrisManager;
    }
    void SetLinearVelocityToZero()
    {
        xspeed = 0;
        yspeed = 0;
        IsStopped = true;
        rigidBody.Sleep();
    }
    public void StartFalling()
    {
        yspeed = 2;
        time = 5;
        rigidBody.WakeUp();
    }
    public void SetSpeed(float velocityX, float velocityY)
    {
        xspeed = 5f * velocityX;
        yspeed = 2f + 5f*velocityY;
    }
}
