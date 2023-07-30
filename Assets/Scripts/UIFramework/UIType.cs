using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储单个UI的信息，包括UI的名字，路径，类型
/// </summary>
public class UIType
{
    /// <summary>
    /// UI名字
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// UI路径
    /// </summary>
    public string Path { get; private set; }
    //private bool init;

    public UIType(string path)
    {
        //init = false;
        Path = path;
        Name = path.Substring(path.LastIndexOf('/') + 1);
    }
}
