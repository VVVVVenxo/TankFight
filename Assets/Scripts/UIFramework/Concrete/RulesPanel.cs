using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 规则面板
/// </summary>
public class RulesPanel : BasePanel
{
    static readonly string path = "Prefabs/UI/Panel/RulesPanel";
    public RulesPanel() : base(new UIType(path))
    {

    }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("EnterButton").onClick.AddListener(() =>
        {
            PanelManager.Pop();
        });
    }



    public override void OnExit()
    {
        UIManager.DestroyUI(UIType);
    }
}
