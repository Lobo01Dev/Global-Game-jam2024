using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleSlime : MonoBehaviour
{

    public SlimePool sp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPool(EnemyHealth value)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        sp.gameObject.SetActive(true);
        sp.StartPool(value);
    }
}
