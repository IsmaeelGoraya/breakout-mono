using UnityEngine;
using System.Collections;

public class PadController : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    private Coroutine moveCoroutine;
    private Vector3 newPos = Vector3.zero;
    private float screenLeft;
    private float screeRight;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        newPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        screenLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).x;
        screenLeft -= spriteRenderer.bounds.min.x;
        screeRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        screeRight -= spriteRenderer.bounds.max.x;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += TouchStarted;
        inputManager.OnEndTouch += TouchEnded;
    }

    private void OnDisabel()
    {
        inputManager.OnStartTouch -= TouchStarted;
        inputManager.OnEndTouch -= TouchEnded;
    }

    private IEnumerator Move()
    {
        while (true) {
            newPos.x = inputManager.PrimaryPosition().x;
            if(newPos.x>screenLeft && newPos.x<screeRight)
            {
                transform.position = newPos;
            }
            yield return null;
        }
    }

    private void TouchStarted(Vector2 xDelta, float time)
    {
        moveCoroutine = StartCoroutine(Move());
    }

    private void TouchEnded(Vector2 xDelta, float time)
    {
        StopCoroutine(moveCoroutine);
    }

}
