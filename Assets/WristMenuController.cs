using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristMenuController : MonoBehaviour
{
    [SerializeField] OVRHand _leftHand;
    [SerializeField] OVRHand _rightHand;

    [SerializeField] OVRPlugin.Hand handType;

    private OVRHand _menuHand;

    // Start is called before the first frame update
    void Start()
    {
        if (handType == OVRPlugin.Hand.HandLeft)
        {
            _menuHand = _leftHand;
        }
        else if (handType == OVRPlugin.Hand.HandRight)
        {
            _menuHand = _rightHand;
        }
        else
        {
            Debug.LogError("Unknown Hand Type");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _menuHand.PointerPose.position;
        transform.rotation = _menuHand.PointerPose.rotation;
    }
}
