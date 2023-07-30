using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 所有UI面板的父类，包含UI面板的状态信息
/// </summary>
public abstract class BasePanel
{
    /// <summary>
    /// UI信息
    /// </summary>
    public UIType UIType { get; private set; }

    /// <summary>
    /// UI管理工具
    /// </summary>
    public UITool UITool { get; private set; }

    /// <summary>
    /// 面板管理器
    /// </summary>
    public PanelManagers PanelManager { get; private set; }

    /// <summary>
    /// UI管理器
    /// </summary>
    public UIManager UIManager { get; private set; }

    public BasePanel(UIType uitype)
    {
        UIType = uitype;
    }

    /// <summary>
    /// 初始化UITool
    /// </summary>
    /// <param name="uiTool"></param>
    public void Initialize(UITool uiTool)
    {
        UITool = uiTool;
    }

    /// <summary>
    /// 初始化面板管理器
    /// </summary>
    /// <param name="panelManager"></param>
    public void Initialize(PanelManagers panelManager)
    {
        PanelManager = panelManager;
    }

    /// <summary>
    /// 初始化UI管理器
    /// </summary>
    /// <param name="uiManager"></param>
    public void Initialize(UIManager uiManager)
    {
        UIManager = uiManager;
    }

    /// <summary>
    /// UI进入时执行的操作，只会执行一次
    /// </summary>
    public virtual void OnEnter()
    {
        //Debug.Log("OnEnter");
    }

    /// <summary>
    /// UI暂停时执行的操作
    /// </summary>
    public virtual void OnPause()
    {
        //UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
    }

    /// <summary>
    /// UI继续时进行的操作
    /// </summary>
    public virtual void OnResume()
    {
        //Debug.Log("OnResume");
    }

    /// <summary>
    /// UI退出时进行的操作
    /// </summary>
    public virtual void OnExit()
    {
        //Debug.Log("OnExit");
    }
}
