using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using OculusSampleFramework;
using UnityEngine.Events;
using DG.Tweening;

public class CubeDebugger : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent Onproximity;
    public UnityEvent OnContact;
    public UnityEvent OnAction;
    public UnityEvent OnExit;

    void Start()
    {
        GetComponent<ButtonListener>().proximityTrigger += OnButtonProximity;
        GetComponent<ButtonListener>().contactTrigger += OnButtonContact;
        GetComponent<ButtonListener>().actionTrigger += OnButtonAction;

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    OnButtonAction();
        //}
    }

   

    public virtual void OnButtonProximity()
    {

    }

    public virtual void OnButtonContact()
    {
    }

    public virtual void OnButtonAction()
    {
        if (UIController.Instance.currentState != UIController.UIstate.General)
        {
            return;
        }
        UIController.Instance.currentFocusPlanet = gameObject;
        PlanetMovement planetMovement = gameObject.GetComponent<PlanetMovement>();
        planetMovement.Ismoveable = false;
        GetComponent<TrailRenderer>().enabled = false;
        UIController.Instance.EnterUIState(UIController.UIstate.ShapeSettings);
        DOTween.Sequence()
            .Append(transform.DOMove(UIController.Instance.ReviewPoint.transform.position,3f).SetEase(Ease.InOutCubic))
            .Join(transform.DOScale(1.5f, 3f));
        this.enabled = false;
    }

    public virtual void OnButtonExit()
    {

    }

}
