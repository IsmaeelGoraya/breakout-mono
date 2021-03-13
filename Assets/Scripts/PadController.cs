using UnityEngine;
using UnityEngine.InputSystem;

public class PadController : MonoBehaviour
{
    public InputAction control;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        control.Enable();
    }

    private void OnDisable()
    {
        control.Disable();
    }

    private void Update()
    {
        MoveHorizontal(control.ReadValue<float>());
        
    }

    public void MoveHorizontal(float xDelta)
    {
        transform.Translate(xDelta * Time.deltaTime, 0, 0);
    }

}
