using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem.Controls;


public enum GameState
{
    PUBLISHER,
    DEVELOPER,
    MAINMENU,
    CONFIG,
    THEMESELECT,
    STAGESELECT,
    INGAME,
    PAUSED,
    SCORE
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameState state = 0;

    //Publics GameObjets

    public static GameManager gm;
    public HUDMainMenu mainMenu;

    //Private GameObjects

    private float timeCount;

    [SerializeField]
    private GameObject player;




    public GameState State { get => state; set => state = value; }
    public GameObject Player { get => player; set => player = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        timeCount = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case GameState.PUBLISHER:
                if (Input.GetMouseButtonDown(0) || timeCount - Time.time < -2)
                {
                    State = GameState.DEVELOPER;
                    timeCount = Time.time;
                }
                break;
            case GameState.DEVELOPER:
                if (Input.GetMouseButtonDown(0) || timeCount - Time.time < -2)
                {
                  //  mainMenu.gameObject.SetActive(true);
                    State = GameState.MAINMENU;
                }
                break;
            case GameState.MAINMENU:
                break;
            case GameState.CONFIG:
                break;
            case GameState.THEMESELECT:
                break;
            case GameState.STAGESELECT:
                break;
            case GameState.INGAME:
                if (Player!=null)
                {
                    if (player.GetComponentInChildren<PlayerHealth>().Health <= 0)
                    {
                        state = GameState.SCORE;
                        Time.timeScale = 0;
                        FindObjectOfType<InGameMenu>().GameOver();
                    }
                }
                break;
            case GameState.PAUSED:
                break;
            case GameState.SCORE:
                break;
        }
    }

}
