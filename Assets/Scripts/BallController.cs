using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float speed;
    [SerializeField]
    private BrickSpawner brickSpawner;
    private Vector2 ballSpawnPosition;

    public delegate void BallDroped();
    public event BallDroped OnBallDropped;


    private void Awake()
    {
        speed = 5;
        rigidBody = GetComponent<Rigidbody2D>();
        Respawn();
    }

    private void Respawn()
    {
        ballSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2f, Screen.height / 8f));
        transform.position = new Vector3();
        rigidBody.velocity = Random.insideUnitCircle.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name.Equals("Bottom"))
        {
            OnBallDropped?.Invoke();
        }
    }
}
