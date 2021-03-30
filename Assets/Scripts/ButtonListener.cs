using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using System;

public class ButtonListener : MonoBehaviour
{
    // Start is called before the first frame update
    public event Action proximityTrigger;
    public event Action contactTrigger;
    public event Action actionTrigger;
    public event Action defaultTrigger;

    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    // Update is called once per frame
    void InitiateEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ProximityState)
        {
            proximityTrigger();
        }
        else if (state.NewInteractableState == InteractableState.ContactState)
        {
            contactTrigger();
        }
        else if (state.NewInteractableState == InteractableState.ActionState)
        {
            actionTrigger();
        }
        else 
        {
            defaultTrigger();
        }
    }
}
