using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Easy = 100, Medium = 200, Hard = 300, Special = 500 };
public enum Direction { Left = -1, Right = 1 };
public class Enemy : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public SceneController Controller;
    public Bullet BulletPrefab;
    public GameObject BulletsRoot;
    public EnemyType Type { get; set; } = EnemyType.Easy;
    public Direction Direction { get; set; } = Direction.Right;
    public int Index { get; set; }
    public bool IsActive { get; set; } = false;
    public float Speed { get; set; } = 0.5F;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {
        Vector3 enemyPos = gameObject.transform.position;
        enemyPos.x = enemyPos.x + (int)Direction * Speed;
        gameObject.transform.position = enemyPos;
    }

    public void MoveForward()
    {
        Vector3 enemyPos = gameObject.transform.position;
        enemyPos.y = enemyPos.y - Speed;
        gameObject.transform.position = enemyPos;
    }

    public void ChangeDirection()
    {
        if (Direction == Direction.Right)
        {
            Direction = Direction.Left;
        }
        else
        {
            Direction = Direction.Right;
        }
    }

    public void Shoot()
    {
        Bullet bullet = Instantiate(BulletPrefab);
        bullet.Parent = this;
        bullet.transform.parent = Controller.transform.GetChild(2).transform;
        Vector3 bulletPos = gameObject.transform.position;
        bulletPos.y -= 0.5F;
        bullet.transform.position = bulletPos;
    }

    public void Destroy()
    {
        if (Type != EnemyType.Special)
        {
            Controller.ActivateEnemy(Index);
        }

        Controller.Score += (int)Type;
        Destroy(gameObject);
    }

}