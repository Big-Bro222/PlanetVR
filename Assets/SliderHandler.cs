using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHandler : MonoBehaviour
{
    [SerializeField] private VRSlider vrSlider;
    // Start is called before the first frame update
    void Start()
    {
        vrSlider = transform.parent.GetComponent<VRSlider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            vrSlider.fingerReference = other.gameObject;
            vrSlider.isActivate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            vrSlider.fingerReference = null;
            vrSlider.isActivate = false;
        }
    }
}
