using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public GameObject BulletsRoot;
    public Bullet BulletPrefab;
    public float Speed = 5F;
    public int Lives = 3;
    private const float LEFT_BOUNDARY = -9.0F;
    private const float RIGHT_BOUNDARY = 9.0F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        Vector3 movement = gameObject.transform.position;
        movement.x += deltaX;

        if (movement.x > RIGHT_BOUNDARY)
        {
            movement.x = RIGHT_BOUNDARY;
        }
        else if (movement.x < LEFT_BOUNDARY)
        {
            movement.x = LEFT_BOUNDARY;
        }

        gameObject.transform.position = movement;
    }

    void Shoot() // Two identical methods in Player.cs and Enemy.cs
    {
        Bullet bullet = Instantiate(BulletPrefab);
        bullet.Parent = this;
        bullet.transform.parent = BulletsRoot.transform;
        Vector3 bulletPos = gameObject.transform.position;
        bulletPos.y += 0.5F;
        bullet.transform.position = bulletPos;
    }
}
