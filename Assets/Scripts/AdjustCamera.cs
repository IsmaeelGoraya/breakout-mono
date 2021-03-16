using UnityEngine;

public class AdjustCamera : MonoBehaviour
{
    private Vector2 targetSize;
    private float screenRatio;
    private float targetRatio;
    private float differenceInSize;
    //Refrence width and height value of boundry in world space.
    private const float WIDTH = 5.8f;
    private const float HEIGHT = 10.0f;

    private void Awake()
    {
        targetSize = new Vector2(WIDTH, HEIGHT);
    }

    void Start()
    {
        AdjustCameraSize();
    }

    private void AdjustCameraSize()
    {
        screenRatio = (float)Screen.width / (float)Screen.height;
        targetRatio = targetSize.x / targetSize.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = targetSize.y / 2;
        }
        else
        {
            differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = targetSize.y / 2 * differenceInSize;
        }
    }
}
