using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class HandTipHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Thumb;
    [SerializeField] GameObject Index;
    [SerializeField] GameObject Middle;
    [SerializeField] GameObject Ring;
    [SerializeField] GameObject Pinky;

    [SerializeField]public OVRSkeleton Skeleton;
    public OVRHand hand;
    OVRBone ThumbBone;
    OVRBone IndexBone;
    OVRBone MiddleBone;
    OVRBone RingBone;
    OVRBone PinkyBone;

    void Start()
    {
        
        ThumbBone=Skeleton.Bones[19];
        IndexBone = Skeleton.Bones[20];
        MiddleBone = Skeleton.Bones[21];
        RingBone = Skeleton.Bones[22];
        PinkyBone = Skeleton.Bones[23];
        hand = Skeleton.GetComponent<OVRHand>();
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateBonePosition(Thumb, ThumbBone);
        UpdateBonePosition(Index, IndexBone);
        UpdateBonePosition(Middle, MiddleBone);
        UpdateBonePosition(Ring, RingBone);
        UpdateBonePosition(Pinky, PinkyBone);

    }

    private void UpdateBonePosition(GameObject boneObj, OVRBone bone)
    {
        if (boneObj!=null&&bone!=null)
        {
            boneObj.transform.position = bone.Transform.position;
            boneObj.transform.rotation = bone.Transform.rotation;
        }

    }
}
