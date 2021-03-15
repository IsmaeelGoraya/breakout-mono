using UnityEngine;
using DataModels;

public class BrickBehaviour : MonoBehaviour
{
    private Brick brickModel;
    private SpriteRenderer spriteRenderer;
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
}
