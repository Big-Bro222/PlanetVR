using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonViewController : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public UnityAction onExit;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnContact()
    {
        meshRenderer.enabled = true;
    }

    public void OnProximity()
    {
        meshRenderer.enabled = false;
        //UIController.Instance.isUISwitchable = true;
    }

}
