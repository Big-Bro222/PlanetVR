using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using OculusSampleFramework;
public class CubeDebugger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void OnButtonProximity()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void OnButtonContact()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public void OnButtonAction()
    {
        GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
}
