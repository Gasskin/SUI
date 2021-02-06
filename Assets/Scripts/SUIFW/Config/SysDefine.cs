/**
    1.常量
    2.全局方法
    3.枚举类型
    4.委托定义
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;



#region 枚举类型
/// <summary>
/// 窗口类型
/// </summary>
public enum UIFormType
{
    /// 普通窗口
    Normal,
    /// 固定窗口
    Fixed,
    /// 弹出窗口
    PopUp,
}

/// <summary>
/// 窗口的显示类型
/// </summary>
public enum UIFormShowMode
{
    /// 普通
    Normal,
    /// 隐藏其它
    HideOther,
    /// 返回
    Reverse
}

/// <summary>
/// UI窗口的透明度
/// </summary>
public enum UIFormLucenyType
{
    /// 全透明，事件不穿透
    Luceny,
    /// 半透明，事件不穿透
    Translucence,
    /// 低透明，事件不穿透
    ImPenetrable,
    /// 事件穿透
    Pentrate
}


#endregion
