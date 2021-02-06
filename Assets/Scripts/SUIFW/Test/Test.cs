using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    UIManager manager;
    void Start()
    {
        manager = UIManager.GetInstance();
        manager.ShowUIForm("LogonUIForm");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
