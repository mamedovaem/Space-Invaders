using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour
{
    public Object Parent;
    public ParentType ParentType;

    // Start is called before the first frame update
    void Start()
    {
       if(GetComponent<Player>()!=null) // How do I call GetComponent only once and do all the necessary stuff?
        {
            Parent = GetComponent<Player>();
            ParentType = ParentType.Player;
        }
       else if(GetComponent<BaseBlock>()!=null)
        {
            Parent = GetComponent<BaseBlock>();
            ParentType = ParentType.Player;
        }
        else if (GetComponent<Enemy>() != null)
        {
            Parent = GetComponent<Enemy>();
            ParentType = ParentType.Enemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
      Bullet bullet = collision.gameObject.GetComponent<Bullet>();

      if (bullet != null && ParentType != bullet.ParentType)
        {
            switch(Parent)
            {
                case Player p:
                    p.Lives--;
                    Destroy(collision.gameObject);
                    break;
                case BaseBlock b:
                    b.Destroy();
                    Destroy(collision.gameObject);
                    break;
                case Enemy en:
                    en.Destroy();
                    Destroy(collision.gameObject);
                    break;
            }
        }
        else if(bullet != null && Parent != bullet.Parent)
        {
            Destroy(collision.gameObject);
        }

    }
}
