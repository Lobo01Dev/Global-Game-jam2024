using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePool : MonoBehaviour
{

    public float poolTime = 5;

    private Collider2D player = null;
    private EnemyHealth myHealth;

    private float cd = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(cd != 0 && cd+poolTime <= Time.time)
        {
            if (player != null)
            {
                player.GetComponent<PlayerController>().MoveSpeedFactor = 1;
            }
            myHealth.DestroyMe();
        }
    }

    public void StartPool(EnemyHealth value)
    {
        myHealth = value;
        cd = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.GetComponent<PlayerController>() != null)
        {
            player = collision;
            collision.GetComponent<PlayerController>().MoveSpeedFactor = 0.1f;
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            playerRb.velocity = playerRb.velocity / 10;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<PlayerController>() != null)
        {
            player = null;
            collision.GetComponent<PlayerController>().MoveSpeedFactor = 1;
        }
    }
}
