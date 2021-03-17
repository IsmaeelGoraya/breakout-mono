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
        //Asuming that only ball is moving and it will be the only collider to ever collide with brick.
        OnBrickDestroyed?.Invoke(this);
        gameObject.SetActive(false);
    }
}
