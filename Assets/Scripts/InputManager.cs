using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    private PadInputs padInputs;
    private Camera mainCamera;

    private void Awake()
    {
        padInputs = new PadInputs();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        padInputs.Enable();
    }

    private void OnDisable()
    {
        padInputs.Enable();
    }

    private void Start()
    {
        padInputs.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        padInputs.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, padInputs.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, padInputs.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, padInputs.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
