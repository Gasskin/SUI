using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCityUIForm : BaseUIForm
{
    private void Awake()
    {
        typeInfo.showMode = UIFormShowMode.HideOther;

        RegisterButtonEvent("store", p => {
            UIManager.GetInstance().ShowUIForm("MarketUIForm");
            }
        );
    }
}
