using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    public float speed = 5;

    private Rigidbody2D rb;
    private float lifeTime = 5;
    private float cd;
    // Start is called before the first frame update
    void Start()
    {
        cd = Time.time;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 direction = mousePosition - GameManager.gm.Player.transform.position; //direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction.normalized*speed,ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        if(cd+lifeTime < Time.time)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(2,Vector3.zero);
            Destroy(gameObject);
        }
        if(collision.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
