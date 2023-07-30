using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 开始场景
/// </summary>
public class StartScene : SceneState
{
    /// <summary>
    /// 场景名称
    /// </summary>
    readonly string sceneName = "Start";
    PanelManagers panelManagers;    // 面板管理器

    public override void OnEnter()
    {
        panelManagers =new PanelManagers();

        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += sceneLoaded;
        }
        else
        {
            panelManagers.Push(new StartPanel());
        }
    }

    public override void OnExit()
    {
        SceneManager.sceneLoaded -= sceneLoaded;
    }

    /// <summary>
    /// 场景加载完毕之后执行的方法
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        panelManagers.Push(new StartPanel());
        Debug.Log($"{scene.name} 场景加载完毕！");
    }
}
