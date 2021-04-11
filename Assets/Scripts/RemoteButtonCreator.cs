using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class RemoteButtonCreator : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject selectionCylinder;
    GameObject Zone;
    SphereCollider parentSphereCollider;
    SphereCollider childSphereCollider;


    ButtonController buttonController;
    void Start()
    {
        creatButtonComponent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void creatButtonComponent()
    {
        Zone = transform.Find("Zone").gameObject;
        buttonController=gameObject.GetComponent<ButtonController>();
        parentSphereCollider = GetComponent<SphereCollider>();

        childSphereCollider = Zone.GetComponent<SphereCollider>();

        childSphereCollider.radius = parentSphereCollider.radius*0.8f;
        gameObject.GetComponent<CubeDebugger>().enabled = false;

    }
}
