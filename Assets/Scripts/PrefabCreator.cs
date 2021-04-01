using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using DG.Tweening;
public class PrefabCreator : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject newPrefab;
    bool grabble;
    Sequence s1;
    Sequence s2;
    GameObject shield;

    void Start()
    {
        grabble = true;
        s1 = DOTween.Sequence();
        s2 = DOTween.Sequence();
        shield = UIController.Instance.sheild;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGrabBegin()
    {
        if (UIController.Instance.currentState==UIController.UIstate.General&& grabble)
        {
            VRDebug.Instance.Log("Begin");
            newPrefab = Instantiate(gameObject, transform.parent);
            newPrefab.transform.position = transform.position;
            newPrefab.transform.localScale = transform.localScale;
            newPrefab.GetComponent<OVRGrabbable>().enabled = false;
            transform.localScale *= 1.5f;
            UIController.Instance.EnterUIState(UIController.UIstate.Selecting);
            UIController.Instance.currentFocusPlanet = gameObject;
            VRDebug.Instance.Log("GrabBeginComplete");

        }


    }

    public void OnGrabEnd()
    {
        if (grabble)
        {
            VRDebug.Instance.Log("End");
            newPrefab.GetComponent<OVRGrabbable>().enabled = true;
            grabble = false;
            //s.Append(UIController.Instance.currentFocusPlanet.transform.DORotate())
            s2.PrependInterval(4);
            s2.Append(transform.DOMove(UIController.Instance.OrbitPoint.transform.position, 3f).SetEase(Ease.InOutCubic));
            s2.Join(transform.DOScale(Vector3.one, 3f).SetEase(Ease.InOutCubic));
            UIController.Instance.EnterUIState(UIController.UIstate.General);
            gameObject.GetComponent<CubeDebugger>().enabled = true;
            grabble = false;
            VRDebug.Instance.Log("GrabEndComplete");

        }

    }

}
