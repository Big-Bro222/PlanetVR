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
    private Sequence s1;
    public UnityEvent Onproximity;
    public UnityEvent OnContact;
    public UnityEvent OnAction;
    public UnityEvent OnExit;

    void Start()
    {
        GetComponent<ButtonListener>().proximityTrigger += OnButtonProximity;
        GetComponent<ButtonListener>().contactTrigger += OnButtonContact;
        GetComponent<ButtonListener>().actionTrigger += OnButtonAction;
        s1 = DOTween.Sequence();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public virtual void OnButtonProximity()
    {
        Onproximity.Invoke();
    }

    public virtual void OnButtonContact()
    {
        OnContact.Invoke();
    }

    public virtual void OnButtonAction()
    {
        UIController.Instance.EnterUIState(UIController.UIstate.ShapeSettings);
        s1.Append(transform.DOMove(UIController.Instance.ReviewPoint.transform.position,3f).SetEase(Ease.InOutCubic));
        s1.Join(transform.DOScale(1.5f, 3f));
        this.enabled = false;
        UIController.Instance.currentFocusPlanet = gameObject;
        OnAction.Invoke();
    }

    public virtual void OnButtonExit()
    {

    }
}
