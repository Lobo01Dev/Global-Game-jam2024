using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneControl : MonoBehaviour
{
    public static SceneControl sc;

    // Start is called before the first frame update
    void Start()
    {
        if (sc == null)
        {
            sc = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }
}
