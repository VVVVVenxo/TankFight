using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// 开始主面板
/// </summary>
public class StartPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/StartPanel";
    public StartPanel() : base(new UIType(path))
    {

    }

    public override void OnEnter()  // 进入面板
    {
        base.OnEnter();
        UITool.GetOrAddComponentInChildren<Button>("rulesButton").onClick.AddListener(() =>
        {
            PanelManager.Push(new RulesPanel());
        });
        
        UITool.GetOrAddComponentInChildren<Button>("startButton").onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameRoot.Instance.sceneSystem.SetScene(new TutorialScene());
        });
        UITool.GetOrAddComponentInChildren<Button>("exitButton").onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }

}
