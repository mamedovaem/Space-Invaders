using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParentType { Player = 0, Enemy = 1 };
public class Bullet : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public Object Parent;
    public float Speed = 5.0F;
    public ParentType ParentType;
    private const float UPPER_BOUNDARY = 5.0F;
    private const float LOWER_BOUNDARY = -5.0F;

    // Start is called before the first frame update
    void Start()
    {
        if(Parent is Player p)
        {
            ParentType = ParentType.Player;
            Sprite.color = p.Sprite.color;        
        }
        else if (Parent is Enemy en)
        {
            ParentType = ParentType.Enemy;
            Sprite.color = en.Sprite.color;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(gameObject.transform.position.y > UPPER_BOUNDARY || gameObject.transform.position.y < LOWER_BOUNDARY)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        float deltaY = Speed * Time.deltaTime;
        Vector3 movement = gameObject.transform.position;
        if (ParentType == ParentType.Player)
        {
            movement.y += deltaY;
        }
        else
        {
            movement.y -= deltaY;
        }
        gameObject.transform.position = movement;
    }
}
