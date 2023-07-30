using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : BasePanel
{

    static readonly string path = "Prefabs/UI/Panel/TutorialPanel";
    public TutorialPanel() : base(new UIType(path))
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Button pauseButton = UITool.GetOrAddComponentInChildren<Button>("PauseButton");
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    private void OnPauseButtonClicked()
    {
        PanelManager.Push(new PausePanel()); // 弹出新界面

        Time.timeScale = 0f; // 暂停游戏，将时间缩放设置为0
    }
}
