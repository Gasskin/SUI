using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketUIForm : BaseUIForm
{
    private void Awake()
    {
        typeInfo.type = UIFormType.PopUp;
        typeInfo.showMode = UIFormShowMode.Reverse;
    }
}
