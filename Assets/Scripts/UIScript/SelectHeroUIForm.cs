using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHeroUIForm : BaseUIForm
{
    private void Awake()
    {
        typeInfo.showMode = UIFormShowMode.HideOther;

        RegisterButtonEvent("OK", m=> {
            UIManager.GetInstance().ShowUIForm("MainCityUIForm");
            UIManager.GetInstance().ShowUIForm("HeroInfoUIForm");
        });
        RegisterButtonEvent("Close", CloseUIForm);
    }

    
}
