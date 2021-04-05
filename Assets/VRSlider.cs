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
    private float scale;
    private const int scaleNum=10;

    void Start()
    {
        scale = maxValue / scaleNum;
        SetValue(sliderValue);
        _sliderValue = sliderValue;
        sliderLength = Vector3.Distance(sliderRightTip.transform.position,sliderLeftTip.transform.position);
        GetComponent<ButtonListener>().proximityTrigger += OnButtonProximity;
        GetComponent<ButtonListener>().contactTrigger += OnButtonContact;
        GetComponent<ButtonListener>().actionTrigger += OnButtonAction;
    }


    public void SetValue(float value)
    {
        if (value > maxValue)
        {
            value = maxValue;
        }
        int scaleProperty = Mathf.FloorToInt(value / scale);
        sliderValue = scale * scaleProperty;
        textMesh.text = (scale*scaleProperty).ToString();
        sliderHandler.transform.position = sliderLeftTip.transform.position +(sliderRightTip.transform.position - sliderLeftTip.transform.position) * scaleProperty/ scaleNum;

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
                    //Value = (float)Math.Round((double)Value, 2);
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
