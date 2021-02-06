using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClotheUIForm : BaseUIForm
{
    private void Awake()
    {
        typeInfo.type = UIFormType.PopUp;
        typeInfo.showMode = UIFormShowMode.Reverse;

        RegisterButtonEvent("close", p =>
        {
            UIManager.GetInstance().CloseUIForm("ClotheUIForm");
        });
    }
}
