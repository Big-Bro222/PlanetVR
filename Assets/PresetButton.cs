using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetButton : CubeDebugger
{
    // Start is called before the first frame update

    [SerializeField]private ButtonViewController buttonViewController;
    void Start()
    {
        buttonViewController.onExit += OnButtonExit;
        GetComponent<ButtonListener>().proximityTrigger += OnButtonProximity;
        GetComponent<ButtonListener>().contactTrigger += OnButtonContact;
        GetComponent<ButtonListener>().actionTrigger += OnButtonAction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnButtonAction()
    {
        OnAction.Invoke();
    }

    public override void OnButtonContact()
    {
        OnContact.Invoke();
    }
    public override void OnButtonProximity()
    {
        Onproximity.Invoke();
    }

    public override void OnButtonExit()
    {
        OnExit.Invoke();
    }
}
