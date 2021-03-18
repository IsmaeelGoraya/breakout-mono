using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float speed;
    private Vector2 ballSpawnPosition;
    private SpriteRenderer spriteRenderer;

    public delegate void BallDroped();
    public event BallDroped OnBallDropped;

    private void Awake()
    {
        speed = 5;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetBall();
    }

    private void ResetBall()
    {
        ballSpawnPosition = new Vector2(0,-3);
        transform.position = ballSpawnPosition;
        rigidBody.velocity = Random.insideUnitCircle.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //Edge collider only exists on the boundry of the scene.
        if(other.gameObject.GetComponent<EdgeCollider2D>())
        {
            if( transform.position.y - spriteRenderer.size.y <= other.collider.bounds.min.y + spriteRenderer.size.y)
            {
                OnBallDropped?.Invoke();
                ResetBall();
            }
        }
    }
}
