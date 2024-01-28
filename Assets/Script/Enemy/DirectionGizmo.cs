using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Mathematics;

public class DirectionGizmo : MonoBehaviour
{
    public float dotDirection;
    public GameObject gizmoTarget;
    public LayerMask rayLayerMask;
    [Range(0.5f,2f)]
    public float rayRange = 0.5f;
    public float circleRange = 0.05f;
    public bool flip = false;


    public float explosionRadius = 0.15f;

    private int angleIncremet = 30;
    private SpriteRenderer sr;
    private Vector2 lastDirection;
    [SerializeField]
    Vector2 bestDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (gizmoTarget == null)
        {
            gizmoTarget = GameManager.gm.Player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gizmoTarget == null)
        {
            gizmoTarget = GameManager.gm.Player;
        }

        
    }

    public Vector2 BestDirection(Vector2 direction)
    {
        int j = 360 / angleIncremet;
        float angle = 0;
        
        float dot;
        
        float bestDot = 0;

        for (int i = 0; i < j; i++)
        {
            bool hitOK = false;
            Vector2 dir = transform.TransformDirection(new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)));

            Ray ray = new Ray(transform.position, dir);
            RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction, rayRange, rayLayerMask);
            foreach (RaycastHit2D r in hit)
            {

                if (r.collider != null && r.collider.GetComponent<DirectionGizmo>() != this)
                {
                    hitOK = true;
                    dot = 0;
                }

            }

            hit = Physics2D.CircleCastAll(transform.transform.position, circleRange, ray.direction, circleRange, rayLayerMask);
            foreach (RaycastHit2D r in hit)
            {
                if (r.collider != null && r.collider.GetComponent<DirectionGizmo>() != this)
                {
                    hitOK = true;
                    dot = 0;
                }
            }

            if (!hitOK)
            {
                Vector2 other = GameManager.gm.Player.transform.position - transform.position;
                dot = Vector2.Dot(ray.direction, other);
                if (dot > bestDot)
                {
                    if (lastDirection.x != (ray.direction.x * -1) && lastDirection.y != (ray.direction.y * -1))
                    {

                        bestDot = dot;
                        bestDirection = ray.direction;
                    }
                }
            }
            angle += angleIncremet;
        }        
        if (bestDirection != Vector2.zero)
        {
            if (lastDirection.x != (bestDirection.x * -1) && lastDirection.y != (bestDirection.y * -1))
            {

                direction = bestDirection;
            }
            else
            {
                direction = lastDirection;
            }
        }
        lastDirection = direction;
        return direction;
    }


    void OnDrawGizmosSelected()
    {
        
        float dot;

        Vector2 forward = transform.TransformDirection(Vector2.right);
        Vector2 other = gizmoTarget.transform.position - transform.position;
        dot = Vector2.Dot(other, forward);

        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);

        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector2 direction = other.normalized * 0.3f;
        Gizmos.DrawRay(transform.position, direction);

        int j = 360 / angleIncremet;
        float angle = 0f; // angle in degrees
        Gizmos.color = Color.blue;
        for (int i = 0; i < j; i++)
        {
            bool hitOK = false;
            angle += angleIncremet;
            Vector2 dir = transform.TransformDirection(new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)));

            Ray ray = new Ray(transform.position, dir);
            RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction, rayRange, rayLayerMask);

            foreach (RaycastHit2D r in hit)
            {
                if (r.collider != null && r.collider.GetComponent<DirectionGizmo>() != this)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(ray.origin, ray.direction.normalized * rayRange);
                    hitOK = true;
                }
            }

            Gizmos.color = Color.grey;
            Gizmos.DrawWireSphere((transform.position + (ray.direction.normalized * circleRange)), circleRange);

            if (!hitOK)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(ray.origin, ray.direction.normalized * rayRange);
            }
        }
    }

   
}
