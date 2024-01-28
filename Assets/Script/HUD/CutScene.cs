using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    public Sprite[] chat;
    public Image chatTextBox;
    public Image chatBoxImage;
    public Image cutSceneBG;

    public InGameMenu HUD;

    private int chatCount;

    // Start is called before the first frame update
    void Start()
    {
        chatCount = 0;
        chatTextBox.sprite = chat[0];
        cutSceneBG.sprite = HUD.cutSceneImage;
        chatCount++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoChat()
    {
        Debug.Log("Click");
        if (chatCount < chat.Length)
        {
            chatBoxImage.sprite = HUD.bossChat;
            Debug.Log("Avancou");
            chatTextBox.sprite = chat[chatCount];
            chatCount++;
        }
        else
        {
            Debug.Log("Acabou");
            GameManager.gm.State = GameState.MAINMENU;
            SceneControl.sc.LoadScene(0);
        }

    }


}
