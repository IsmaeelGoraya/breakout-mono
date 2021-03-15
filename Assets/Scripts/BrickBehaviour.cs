using UnityEngine;
using DataModels;

public class BrickBehaviour : MonoBehaviour
{
    private Brick brickModel;
    private SpriteRenderer spriteRenderer;
    public delegate void BrickDestroyed(BrickBehaviour self);
    public event BrickDestroyed OnBrickDestroyed;

    public Brick BrickModel {
        get
        {
            return brickModel;
        }
        set
        {
            brickModel = value;
            spriteRenderer.color = brickModel.Color;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnBrickDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
