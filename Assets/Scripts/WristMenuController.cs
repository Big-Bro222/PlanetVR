using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristMenuController : MonoBehaviour
{
    [SerializeField] OVRHand _leftHand;
    [SerializeField] OVRHand _rightHand;

    [SerializeField] OVRPlugin.Hand handType;

    private OVRHand _menuHand;
    private OVRBone _menuBone;
    private GameObject[] BoneIndicatior;

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


        //int BoneIndex = (int)OVRSkeleton.BoneId.Hand_WristRoot;
        //Debug.Log(BoneIndex);
        _menuBone = _menuHand.GetComponent<OVRSkeleton>().Bones[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (_menuBone != null)
        {
            transform.position = _menuBone.Transform.position;
            transform.rotation = _menuBone.Transform.rotation;
        }
    }

        //foreach(OVRBone bone in skeleton.Bones) {
        //if (bone.Id == OVRSkeleton.BoneId.Hand_IndexTip) {
        //    bone.Transform.gameObject.AddComponent<SphereCollider>();
        //}
    //}
}
