using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    public float attackDistance = 0.5f;
    public float dashSpeed = 10;
    public GameObject target;
    public float cooldown = 5;

    private Rigidbody2D rb;
    private float cd;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GameManager.gm.Player != null)
        {
            target = GameManager.gm.Player;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target == null)
        {
            target = GameManager.gm.Player;
        }
        if (Vector2.Distance(target.transform.position, transform.position) <= attackDistance && cd+cooldown <= Time.time)
        {
            cd = Time.time;
            Vector2 other = target.transform.position - transform.position;
            Vector2 dash = other.normalized * attackDistance*dashSpeed;
            rb.AddForce(dash, ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {

        Vector2 other = target.transform.position - transform.position;
        
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector2 direction = other.normalized * attackDistance;
        Gizmos.DrawRay(transform.position, direction);
    }
}