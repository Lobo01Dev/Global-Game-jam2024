using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnStatus
{
    Aleatorio,
    Focado,
    Misto
}

public class SpawnControl : MonoBehaviour
{

    public static SpawnControl spawnCtrl;


    public int maxEnemies = 20;
    public int ThemeChangeTime = 30;
    public GameObject enemieParent;
    public SpawnPoint[] spawnPoints = new SpawnPoint[8];

    private bool canSwpan = true;
    private SpawnStatus status = SpawnStatus.Aleatorio;
    private float cd;
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    public bool CanSwpan { get => canSwpan;}

    // Start is called before the first frame update
    void Start()
    {
        if(spawnCtrl!= null)
        {
            if(spawnCtrl != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            spawnCtrl = this;
        }
        cd = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (cd+ThemeChangeTime < Time.time)
        {
            cd = Time.time;
            int rand = Random.Range(0,3);
            status = (SpawnStatus)rand;

            switch (status)
            {
                case SpawnStatus.Aleatorio:
                    foreach (SpawnPoint p in spawnPoints)
                    {
                        p.Status = SpawnStatus.Aleatorio;
                    }
                    break;
                case SpawnStatus.Focado:
                    int rand1 = Random.Range(0,3);
                    foreach (SpawnPoint p in spawnPoints)
                    {
                        p.Status = SpawnStatus.Focado;
                        p.SetEnemie((Enemie) rand1);
                    }
                    break;
                case SpawnStatus.Misto:
                    foreach (SpawnPoint p in spawnPoints)
                    {
                        int rand2 = Random.Range(0, 3);
                        p.Status = SpawnStatus.Misto;
                        p.SetEnemie((Enemie)rand2);
                    }
                    break;
            }
        }    
    }

    public void AddToList(GameObject value)
    {
        enemies.Add(value);
        if (enemies.Count >= maxEnemies)
        {
            canSwpan = false;
        }
    }

    public void RemoveFromList(GameObject value)
    {
        enemies.Remove(value);
        if (enemies.Count < maxEnemies)
        {
            canSwpan = true;
        }
    }


}
