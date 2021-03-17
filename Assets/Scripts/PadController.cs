using UnityEngine;
using System.Collections;

public class PadController : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    private Coroutine moveCoroutine;
    private Vector3 newPos = Vector3.zero;

    private void Awake()
    {
        newPos = transform.position;
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
            transform.position = newPos;
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
