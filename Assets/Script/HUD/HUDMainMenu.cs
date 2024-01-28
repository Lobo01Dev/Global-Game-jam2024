using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HUDMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton(){
        SceneControl.sc.LoadScene(0);
    }

    public void StartButton(int index){
        GameManager.gm.State = GameState.INGAME;
        SceneControl.sc.LoadScene(index);
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("gm", 0);
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }

#else
Application.Quit();
#endif
    }
}
