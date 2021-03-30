using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
public class PrefabCreator : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject newPrefab;
    bool grabble;
    void Start()
    {
        grabble = true;
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
            grabble = false;
        }


    }

    public void OnGrabEnd()
    {
        if (grabble)
        {
            VRDebug.Instance.Log("End");
            newPrefab.GetComponent<OVRGrabbable>().enabled = true;
            Invoke("setpos", 3f);
            UIController.Instance.EnterUIState(UIController.UIstate.General);
        }
        
    }

    private void setpos()
    {
        UIController.Instance.currentFocusPlanet.transform.position = UIController.Instance.OrbitPoint.transform.position;
    }

}
