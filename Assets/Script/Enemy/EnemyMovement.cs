using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Public Fields

    public float moveSpeed = 1f;
    public bool flip = false;
    public bool useGizMo = false;
    public float bossChasseRadius = 0.5f;

    //Private Fields
    private Rigidbody2D rb;
    private DirectionGizmo dg;
    private EnemyHealth myHealth;
    private Vector3 startPosition;
    [SerializeField]
    private bool chase = false;
    private SpriteRenderer sr;

    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }

    // Start is called before the first frame update
    void Start()
    {
        chase = false;
        sr = GetComponent<SpriteRenderer>();
        myHealth = GetComponent<EnemyHealth>();
        if (myHealth.boss)
        {
            startPosition = transform.position;
        }
        rb = GetComponent<Rigidbody2D>();
        dg = GetComponent<DirectionGizmo>();
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!myHealth.boss)
        {            
            Move();
        }
        else
        {
            chase = false;
            RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, bossChasseRadius, Vector2.zero);
            foreach (RaycastHit2D h in hit)
            {
                if(h.collider.GetComponent<PlayerHealth>() != null)
                {
                    chase = true;
                    Move();
                }
            }
            if (!chase)
            {
                if (Vector2.Distance(startPosition, transform.position) > 0.05f)
                {
                    Flip(startPosition);
                }                
                Vector2 direction = (startPosition - transform.position).normalized;
                rb.AddForce(direction * (moveSpeed* 2 * 10) * Time.deltaTime);

            }
        }
    }

    private void Flip(Vector3 target)
    {
        Vector2 forward = transform.TransformDirection(Vector2.right);
        Vector2 other = target - transform.position;
        float dotDirection = Vector2.Dot(other, forward);
        if (dotDirection >= 0)
        {
            if (flip)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }

        }
        else
        {
            if (flip)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }

    private void Move()
    {
        Flip(GameManager.gm.Player.transform.position);
        Vector2 direction = (GameManager.gm.Player.transform.position - transform.position).normalized;
        if (useGizMo)
        {
            direction = dg.BestDirection(direction);
        }
        rb.AddForce(direction * (moveSpeed * 10) * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, bossChasseRadius);
    }
    

}
