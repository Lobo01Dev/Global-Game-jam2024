using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    public bool canknockback;
    public float knockbackForce = 10;
    public float distanceToKill = 1.25f;
    public bool boss = false;


    private Rigidbody2D rb;

    public int Health
    {
        get {return health;}
        set 
        { 
            health = value;
            if (health <= 0)
            {
                if(GetComponent<PurpleSlime>() != null)
                {
                    GetComponent<PurpleSlime>().StartPool(this);
                }
                else
                {
                    DestroyMe();
                }
                
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (SpawnControl.spawnCtrl != null)
        {
            SpawnControl.spawnCtrl.AddToList(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.Player != null)
        {

            if (distanceToKill < Vector2.Distance(transform.position, GameManager.gm.Player.gameObject.transform.position) && !boss)
            {
                DestroyMe();
            }
        }
    }

    public void TakeDamage(int damage, Vector3 knockbackDirection)
    {
        Health -= damage;
        if (canknockback && health > 0 && knockbackDirection != Vector3.zero) 
        {

            Vector2 knockback = (transform.position - knockbackDirection).normalized * (knockbackForce / 10);
            rb.AddForce(knockback,ForceMode2D.Impulse);
        }
    }

    public void DestroyMe()
    {
        if (GetComponent<CreateCheckPoint>() != null)
        {
            GetComponent<CreateCheckPoint>().CheckPoint(GetComponent<EnemyMovement>().StartPosition);
        }
        SpawnControl.spawnCtrl.RemoveFromList(this.gameObject);
        Destroy(gameObject);
    }

}
