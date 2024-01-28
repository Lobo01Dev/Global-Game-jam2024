using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameMenu : MonoBehaviour
{

    public Slider playerHealth;

    public Sprite pauseButtonImg;
    public Sprite playButtonImg;
    public Button playPauseButton;
    public GameObject pausedMenu;
    public GameObject gameOverMenu;
    public GameObject chatMenu;
    public GameObject cutScene;
    public Sprite cutSceneImage;

    public Image chatBar;
    public Sprite playerChat;
    public Sprite callerChat;
    public Sprite bossChat;


    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        cutScene.SetActive(true);
    }

    public void PlayPauseGame()
    {
        if(GameManager.gm.State == GameState.PAUSED)
        {
            GameManager.gm.State = GameState.INGAME;
            playPauseButton.image.sprite = pauseButtonImg;
            Time.timeScale = 1;
            pausedMenu.SetActive(false);
        }
        else if (GameManager.gm.State == GameState.INGAME)
        {
            GameManager.gm.State = GameState.PAUSED;
            playPauseButton.image.sprite = playButtonImg;
            Time.timeScale = 0;
            pausedMenu.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);

    }

    public void YesButton()
    {

        Time.timeScale = 1;
        GameManager.gm.Player.GetComponentInChildren<PlayerHealth>().Health = 40;
        GameManager.gm.State = GameState.INGAME;
        SceneControl.sc.ReloadScene(); 
    }
    public void NoButton()
    {

        Time.timeScale = 1;
        GameManager.gm.State = GameState.MAINMENU;
        SceneControl.sc.LoadScene(0);    
    }

}
