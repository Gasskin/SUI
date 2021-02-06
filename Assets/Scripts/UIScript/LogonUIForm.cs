using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogonUIForm : BaseUIForm
{
    private void Awake()
    {
        typeInfo.isClearStack = false;
        typeInfo.type = UIFormType.Normal;
        typeInfo.showMode = UIFormShowMode.Normal;
        typeInfo.lucenyType = UIFormLucenyType.Luceny;

        RegisterButtonEvent("OK", OpenHero);
    }

    private void OpenHero(GameObject go)
    {
        UIManager.GetInstance().ShowUIForm("SelectHeroUIForm");
    }
}
