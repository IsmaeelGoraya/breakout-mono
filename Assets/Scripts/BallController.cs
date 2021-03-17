using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float speed;
    private Vector2 ballSpawnPosition;

    public delegate void BallDroped();
    public event BallDroped OnBallDropped;

    private void Awake()
    {
        speed = 5;
        rigidBody = GetComponent<Rigidbody2D>();
        Reset();
    }

    private void Reset()
    {
        ballSpawnPosition = new Vector2(0,-3);
        transform.position = ballSpawnPosition;
        rigidBody.velocity = Random.insideUnitCircle.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name.Equals("Bottom"))
        {
            OnBallDropped?.Invoke();
            Reset();
        }
    }
}
