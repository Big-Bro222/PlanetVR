using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using TMPro;

public class VRSlider : MonoBehaviour
{
    [HideInInspector]
    public OVRSkeleton skeleton;
    [SerializeField] float maxValue=1;
    public float sliderValue;
    [SerializeField] GameObject sliderHandler;
    [SerializeField] GameObject sliderLeftTip;
    [SerializeField] GameObject sliderRightTip;
    [SerializeField] TextMeshPro textMesh;
    public GameObject fingerReference;
    public bool isActivate;

    private float sliderLength;
    void Start()
    {
        SetValue(sliderValue);
        sliderLength = (sliderRightTip.transform.position - sliderLeftTip.transform.position).magnitude;
        GetComponent<ButtonListener>().proximityTrigger += OnButtonProximity;
        GetComponent<ButtonListener>().contactTrigger += OnButtonContact;
        GetComponent<ButtonListener>().actionTrigger += OnButtonAction;
    }


    public void SetValue(float value)
    {
        textMesh.text = value.ToString();
        float distanceproperty = value / maxValue;
        sliderHandler.transform.position = sliderLeftTip.transform.position +(sliderRightTip.transform.position - sliderLeftTip.transform.position) * distanceproperty;

    }
    // Update is called once per frame
    void Update()
    {
        if (fingerReference)
        {
                    if (isActivate)
        {
            Vector3 fingerLeftTipOff = fingerReference.transform.position - sliderLeftTip.transform.position;
            Vector3 projectOff = Vector3.Project(fingerLeftTipOff, transform.right);
            float direction=Vector3.Dot(projectOff, transform.right);
            if (direction > 0)
            {
                float length = projectOff.magnitude;
                float Value = length / sliderLength * maxValue;
                if (Value > maxValue)
                {
                    Value = maxValue;
                }
                SetValue(Value);
            }
            else
            {
                SetValue(0);
            };
        }
        }

    }

    public void OnButtonProximity()
    {
        VRDebug.Instance.Log("OnButtonProximity");
    }

    public void OnButtonContact()
    {
        VRDebug.Instance.Log("OnButtonContact");
    }

    public void OnButtonAction()
    {
        VRDebug.Instance.Log("OnButtonAction");
    }
}
