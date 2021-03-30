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
    Sequence s;
    void Start()
    {
        grabble = true;
        s = DOTween.Sequence();

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
            s.PrependInterval(4);
            s.Append(UIController.Instance.currentFocusPlanet.transform.DOMove(UIController.Instance.OrbitPoint.transform.position, 3f).SetEase(Ease.InOutCubic));
            s.Join(UIController.Instance.currentFocusPlanet.transform.DOScale(Vector3.one,3f).SetEase(Ease.InOutCubic));
            VRDebug.Instance.Log("TweenEnd");
            s.OnComplete(() => {
                UIController.Instance.EnterUIState(UIController.UIstate.General);
                gameObject.GetComponent<CubeDebugger>().enabled = true;
            });
            

        }

    }

}
