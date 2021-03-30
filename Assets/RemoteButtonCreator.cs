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
        buttonController=gameObject.GetComponent<ButtonController>();
        parentSphereCollider = GetComponent<SphereCollider>();

        gameObject.AddComponent<CubeDebugger>();
        buttonController._buttonPlaneCenter = transform;

        Zone = transform.Find("Zone").gameObject;
        Zone.transform.localPosition = Vector3.zero;

        ButtonTriggerZone triggerZone=Zone.GetComponent<ButtonTriggerZone>();
        triggerZone._parentInteractableObj = gameObject;
        Rigidbody rig=Zone.GetComponent<Rigidbody>();
        rig.useGravity = true;
        rig.isKinematic = true;

        buttonController._proximityZone = Zone;
        buttonController._contactZone = Zone;
        buttonController._actionZone = Zone;
        childSphereCollider = Zone.GetComponent<SphereCollider>();

        childSphereCollider.radius = parentSphereCollider.radius;
        childSphereCollider.isTrigger = true;

        gameObject.AddComponent<ButtonListener>();

        gameObject.GetComponent<CubeDebugger>().enabled = false;

    }
}
