using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage;
    public bool selfKnockback;
    public float KnockbackForce = 5;
    Rigidbody2D rb;

    private float cd;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            cd = Time.time;
            // Deal damage to the enemy
            PlayerHealth player = other.collider.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(damage, transform.position);
                if (selfKnockback)
                {
                    Vector2 knockback = (transform.position - player.transform.position).normalized*(KnockbackForce/10);
                    rb.AddForce(knockback, ForceMode2D.Impulse);
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if(cd + 0.25f < Time.time)
        {
            if (other.collider.tag == "Player")
            {
                cd = Time.time;
                // Deal damage to the enemy
                PlayerHealth player = other.collider.GetComponent<PlayerHealth>();

                if (player != null)
                {
                    player.TakeDamage(damage, transform.position);
                    if (selfKnockback)
                    {
                        Vector2 knockback = (transform.position - player.transform.position).normalized * (KnockbackForce / 10);
                        rb.AddForce(knockback, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }
}
