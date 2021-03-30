using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using OculusSampleFramework;
using UnityEngine.Events;

public class CubeDebugger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ButtonListener>().proximityTrigger += OnButtonProximity;
        GetComponent<ButtonListener>().contactTrigger += OnButtonContact;
        GetComponent<ButtonListener>().actionTrigger += OnButtonAction;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void OnButtonProximity()
    {
        VRDebug.Instance.Log("OnButtonProximity");
    }

    public void OnButtonContact()
    {
        VRDebug.Instance.Log("OnButtonContact");
    }

    public void OnButtonAction()
    {
        VRDebug.Instance.Log("OnButtonAction");
        UIController.Instance.EnterUIState(UIController.UIstate.ShapeSettings);
        this.enabled = false;
    }
}
