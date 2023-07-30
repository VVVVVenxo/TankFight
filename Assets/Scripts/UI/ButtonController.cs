using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour
{
    private Button startButton;
    private Button exitButton;
    private Button rulesButton;


    void Start()
    {
        startButton = transform.Find("startButton").GetComponent<Button>();
        exitButton = transform.Find("exitButton").GetComponent<Button>();
        startButton.onClick.AddListener(StartButtonClick);                      //监听函数
        exitButton.onClick.AddListener(ExitButtonClick);
    }
    // 开始游戏
    private void StartButtonClick()
    {
        SceneManager.LoadScene("Tutorial");
    }

    //退出游戏(宏定义实现)
    private void ExitButtonClick()
    {
#if UNITY_EDITOR        //Unity编辑器中调试使用
        EditorApplication.isPlaying = false;
#else                   //导出游戏包后使用
        Application.Quit();
#endif
    }

    public void onGameStart()
    {

    }

    public void onGameRestart()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
