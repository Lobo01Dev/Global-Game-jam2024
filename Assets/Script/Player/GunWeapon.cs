using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GunWeapon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject muzzle;
    public float ShootCd = 2;

    private SpriteRenderer sr;
    private float angle;
    private float cd = 0;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 direction = mousePosition - transform.position;
        angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
        Flip();
    }

    private void Flip()
    {
        Vector2 forward = transform.TransformDirection(Vector2.right); ;
        Vector2 other = (new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().x) - transform.parent.transform.position).normalized;
        float dotDirection = Vector2.Dot(other, forward);
        if (dotDirection >= 0)
        {
            sr.flipY = false;
        }
        else
        {
            sr.flipY = true;
        }
    }

    public void Fire()
    {
        if (cd+ShootCd < Time.time)
        {
            cd = Time.time;
            Instantiate(bullet, muzzle.transform.position, transform.rotation);
        }
    }
}
