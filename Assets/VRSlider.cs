using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using TMPro;
using UnityEngine.Events;
using System;

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
    public UnityAction<float> onsliderChange; 
    private float sliderLength;
    private float _sliderValue;
    void Start()
    {
        SetValue(sliderValue);
        _sliderValue = sliderValue;
        sliderLength = (sliderRightTip.transform.position - sliderLeftTip.transform.position).magnitude;
        GetComponent<ButtonListener>().proximityTrigger += OnButtonProximity;
        GetComponent<ButtonListener>().contactTrigger += OnButtonContact;
        GetComponent<ButtonListener>().actionTrigger += OnButtonAction;
    }


    public void SetValue(float value)
    {
        sliderValue = value;
        textMesh.text = value.ToString();
        float distanceproperty = value / maxValue;
        if (distanceproperty > 1)
        {
            distanceproperty = 1;
        }
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
                Value = (float)Math.Round((double)Value, 2);
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

    private void LateUpdate()
    {
        if (_sliderValue != sliderValue)
        {
            onsliderChange(sliderValue);
            _sliderValue = sliderValue;
        }
    }

    public void OnButtonProximity()
    {
    }

    public void OnButtonContact()
    {
    }

    public void OnButtonAction()
    {
    }
}
