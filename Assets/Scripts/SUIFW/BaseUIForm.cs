using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIForm : MonoBehaviour
{
    #region Private
    public UIType typeInfo = new UIType();

    #endregion

    protected void RegisterButtonEvent(string buttonName,EventTriggerListener.VoidDelegate handle)
    {
        Transform btn = transform.Find(buttonName);
        EventTriggerListener.Get(btn.gameObject).onClick = handle;
    }

    #region 生命周期
    /// <summary>
    /// 显示UI
    /// </summary>
    public virtual void Display()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏UI
    /// </summary>
    public virtual void Hiding()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 重新显示UI，即从隐藏状态切换回显示
    /// </summary>
    public virtual void ReDisplay()
    {
        gameObject.SetActive(true);
    }


    /// <summary>
    /// 冻结窗口，可以在这里取消监听和逻辑处理
    /// </summary>
    public virtual void Freeze()
    {
        gameObject.SetActive(true);
    }
    #endregion

    protected void CloseUIForm(GameObject o)
    {
        string str = GetType().ToString();
        UIManager.GetInstance().CloseUIForm(str);
    }
}
