using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider UISlider;

    private const int _health = 40;

    private int health = _health;

    public int Health { get { return health; } 
        set { health = value;if(health <0) health = 0 ;UISlider.value = health; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage,Vector2 knockback)
    {
        Health -= damage;

    }
}
