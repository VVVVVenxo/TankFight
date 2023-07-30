using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanel : BasePanel     // 继承自BasePanel
{
    static readonly string path = "Prefabs/UI/Panel/PausePanel";
    public PausePanel() : base(new UIType(path))
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        UITool.GetOrAddComponentInChildren<Button>("ContinueButton").onClick.AddListener(OnContinueButtonClicked);


        UITool.GetOrAddComponentInChildren<Button>("RestartButton").onClick.AddListener(() =>
        {
            PanelManager.Pop();
            Time.timeScale = 1f;
            SceneManager.LoadScene("Tutorial");
        });
    

        UITool.GetOrAddComponentInChildren<Button>("MenuButton").onClick.AddListener(() =>
        {
            PanelManager.Pop();
            GameRoot.Instance.sceneSystem.SetScene(new StartScene());
        });
    }

    public override void OnExit()
    {
        UIManager.DestroyUI(UIType);
    }

    private void OnContinueButtonClicked()
    {
        PanelManager.Pop(); // 关闭PausePanel
        Time.timeScale = 1f; // 恢复游戏，将时间缩放设置为1
    }
}
