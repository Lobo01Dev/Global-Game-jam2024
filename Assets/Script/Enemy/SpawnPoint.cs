using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemie
{
    EvilEye,
    DevilDog,
    Slime
}

public class SpawnPoint : MonoBehaviour
{

    public int spawnTime = 10;
    public int variantTime = 0;
    public int enemieQtd = 10;
    public GameObject enemieParent;
    public bool especifcEnemie = false;
    public Enemie thisEnemie;
    public GameObject[] enemies = new GameObject[3];


    private float spawnCd;
    private SpawnStatus status;
    private int variantTimeDelta = 0;

    public SpawnStatus Status
    {
        get { return status; }
        set
        {
            status = value;
            if (status == SpawnStatus.Aleatorio)
            {
                especifcEnemie = false;
            }
            else { especifcEnemie = true; }
            if(variantTime != 0)
            {
                variantTimeDelta = Random.Range(variantTimeDelta*-1, variantTimeDelta + 1);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnControl.spawnCtrl.CanSwpan)
        {
            if (especifcEnemie)
            {
                Spawn((int)thisEnemie);
            }
            else
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        if (spawnCd + spawnTime+variantTimeDelta <= Time.time)
        {
            spawnCd = Time.time;
            for (int i = 0; i < enemieQtd; i++)
            {
                float randi = Random.Range(-0.2f, 0.2f);
                int rand = Random.Range(0, 3);
                Instantiate(enemies[rand], transform.position + Vector3.one * (randi / 10), Quaternion.Euler(Vector3.zero),enemieParent.transform);
            }
        }
    }

    private void Spawn(int value)
    {
        if (spawnCd + spawnTime+variantTimeDelta <= Time.time)
        {
            spawnCd = Time.time;
            for (int i = 0; i < enemieQtd; i++)                
            {
                float randi = Random.Range(-0.2f, 0.2f);
                Instantiate(enemies[value], transform.position+ Vector3.one*(randi / 10), Quaternion.Euler(Vector3.zero), enemieParent.transform);
            }
        }
    }

    public void SetEnemie(Enemie value)
    {
        thisEnemie = value;
    }
}
