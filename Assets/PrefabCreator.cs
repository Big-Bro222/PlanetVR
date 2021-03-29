using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
public class PrefabCreator : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject newPrefab;
    bool prefabDuplicatable;
    void Start()
    {
        prefabDuplicatable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGrabBegin()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
        if (prefabDuplicatable)
        {
            newPrefab = Instantiate(gameObject, transform.parent);
            newPrefab.transform.position = transform.position;
            newPrefab.transform.localScale = transform.localScale;
            newPrefab.GetComponent<OVRGrabbable>().enabled = false;
            transform.localScale *= 1.5f;
            prefabDuplicatable = false;
        }


    }

    public void OnGrabEnd()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        newPrefab.GetComponent<OVRGrabbable>().enabled = true;
    }

}
