using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager:MonoBehaviour
{
  
    private static UIManager instance;
    private Dictionary<string, string> dicFormsPath;
    private Dictionary<string, BaseUIForm> dicAllUIForms;
    private Dictionary<string, BaseUIForm> dicCurrentShowUIForms;
    private Stack<BaseUIForm> staCurrentUIForms;
    private Transform canvasTransform;
    private Transform normalTransform;
    private Transform fixedTransform;
    private Transform popUpTransform;
    private Transform scriptMgr;

    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            instance = new GameObject("UIManager").AddComponent<UIManager>();
            DontDestroyOnLoad(instance);
        }
        return instance;
    }

    private void Awake()
    {
        dicFormsPath = new Dictionary<string, string>();
        dicAllUIForms = new Dictionary<string, BaseUIForm>();
        dicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
        staCurrentUIForms = new Stack<BaseUIForm>();

        canvasTransform = ResourcesMgr.GetInstance().LoadAsset("Prefabs/Canvas", false).transform;
        normalTransform = canvasTransform.Find("Normal");
        fixedTransform = canvasTransform.Find("Fixed");
        popUpTransform = canvasTransform.Find("PopUp");
        scriptMgr = canvasTransform.Find("ScriptMgr");
        if (canvasTransform == null || normalTransform == null || fixedTransform == null || popUpTransform == null || scriptMgr == null) 
        {
            throw new System.Exception("初始化失败");
        }

        canvasTransform.SetParent(transform);

        dicFormsPath.Add("LogonUIForm", "Prefabs/LogonUIForm");
        dicFormsPath.Add("SelectHeroUIForm", "Prefabs/SelectHeroUIForm");
        dicFormsPath.Add("MainCityUIForm", "Prefabs/MainCityUIForm");
        dicFormsPath.Add("HeroInfoUIForm", "Prefabs/HeroInfoUIForm");
        dicFormsPath.Add("MarketUIForm", "Prefabs/MarketUIForm"); 
        dicFormsPath.Add("ClotheUIForm", "Prefabs/ClotheUIForm");
    }

    public void ShowUIForm(string name)
    {
        BaseUIForm baseUIForm;
        baseUIForm = LoadFormToAllUIFormsCache(name);

        if (baseUIForm.typeInfo.isClearStack)
        {
            if (staCurrentUIForms.Count >= 1)
            {
                staCurrentUIForms.Clear();
            }
        }

        switch (baseUIForm.typeInfo.showMode)
        {
            case UIFormShowMode.HideOther:
                EnterUIFormAndHideOther(name);
                break;
            case UIFormShowMode.Normal:
                LoadUIFormToCurrentCache(name);
                break;
            case UIFormShowMode.Reverse:
                PushUIFormToStack(name);
                break;
        }
    }

    private BaseUIForm LoadFormToAllUIFormsCache(string name)
    {
        BaseUIForm ui;
        // 如果还没有加载过
        if (!dicAllUIForms.TryGetValue(name, out ui))
        {
            ui = LoadUIForm(name);
        }
        return ui;
    }

    private BaseUIForm LoadUIForm(string name)
    {
        string path;
        GameObject prefab;
        BaseUIForm script;

        if(dicFormsPath.TryGetValue(name,out path))
        {
            prefab = ResourcesMgr.GetInstance().LoadAsset(path, false);
            if (prefab == null)
            {
                throw new Exception("加载 panel prefab 失败");
            }
            script =(BaseUIForm)prefab.AddComponent(Type.GetType(name));
            if (script == null)
            {
                throw new Exception("加载 panel script 失败");
            }
            switch (script.typeInfo.type) 
            {
                case UIFormType.Fixed:
                    prefab.transform.SetParent(fixedTransform);
                    break;
                case UIFormType.Normal:
                    prefab.transform.SetParent(normalTransform);
                    break;
                case UIFormType.PopUp:
                    prefab.transform.SetParent(popUpTransform);
                    break;
            }
            prefab.transform.localPosition = new Vector2(0, 0);
            prefab.SetActive(false);
            dicAllUIForms.Add(name, script);
            return script;
        }
        throw new Exception(name+"没有注册");
    }

    private void LoadUIFormToCurrentCache(string name)
    {
        BaseUIForm baseUIForm;
        BaseUIForm baseUIFormFromAllCache;
        if(!dicCurrentShowUIForms.TryGetValue(name, out baseUIForm))
        {
            if(dicAllUIForms.TryGetValue(name, out baseUIFormFromAllCache))
            {
                baseUIFormFromAllCache.Display();
                dicCurrentShowUIForms.Add(name,baseUIFormFromAllCache);
            }
        }
    }

    private void PushUIFormToStack(string name)
    {
        if (staCurrentUIForms.Count > 0)
        {
            staCurrentUIForms.Peek().Freeze();
        }
        BaseUIForm ui;
        if(dicAllUIForms.TryGetValue(name,out ui))
        {
            ui.Display();
            staCurrentUIForms.Push(ui);
        }
        else
        {
            throw new Exception("PushUIFormToStack：数据未加载");
        }
    }

    private void EnterUIFormAndHideOther(string name)
    {
        BaseUIForm ui;
        if (!dicCurrentShowUIForms.TryGetValue(name, out ui))
        {
            if (dicAllUIForms.TryGetValue(name, out ui))
            {
                ui.Display();
                foreach (BaseUIForm e in dicCurrentShowUIForms.Values)
                {
                    e.Hiding();
                }
                foreach(BaseUIForm e in staCurrentUIForms)
                {
                    e.Hiding();
                }
                dicCurrentShowUIForms.Add(name, ui);
            }
        }
    }

    public void CloseUIForm(string name)
    {
        BaseUIForm ui;
        if(dicAllUIForms.TryGetValue(name,out ui))
        {
            switch (ui.typeInfo.showMode)
            {
                case UIFormShowMode.HideOther:
                    ExitUIFormsAndDisplayOther(name);
                    break;
                case UIFormShowMode.Normal:
                    ExitUIForms(name);
                    break;
                case UIFormShowMode.Reverse:
                    PopUIForms(name);
                    break;
            }
        }
    }

    private void ExitUIForms(string name)
    {
        BaseUIForm ui;
        if(dicCurrentShowUIForms.TryGetValue(name,out ui))
        {
            ui.Hiding();
            dicCurrentShowUIForms.Remove(name);
        }
    }

    private void PopUIForms(string name)
    {
        if (staCurrentUIForms.Count == 1)
        {
            staCurrentUIForms.Pop().Hiding();
        }
        else if(staCurrentUIForms.Count >=2)
        {
            staCurrentUIForms.Pop().Hiding();
            staCurrentUIForms.Peek().ReDisplay();
        }
    }

    private void ExitUIFormsAndDisplayOther(string name)
    {

        BaseUIForm ui;
        if(dicCurrentShowUIForms.TryGetValue(name,out ui))
        {
            foreach (BaseUIForm e in dicCurrentShowUIForms.Values)
            {
                e.ReDisplay();
            }
            foreach (BaseUIForm e in staCurrentUIForms)
            {
                e.ReDisplay();
            }
            ui.Hiding();
            dicCurrentShowUIForms.Remove(name);
        }
    }


    private int ShowAllUIFormCount()
    {
        if (dicAllUIForms != null)
        {
            return dicAllUIForms.Count;
        }
        return -1;
    }

    private int ShowCurrentUIFormCount()
    {
        if (dicCurrentShowUIForms != null)
        {
            return dicCurrentShowUIForms.Count;
        }
        return -1;
    }
    private int ShowCurrentStackUIFormCount()
    {
        if (staCurrentUIForms != null)
        {
            return staCurrentUIForms.Count;
        }
        return -1;
    }
}
