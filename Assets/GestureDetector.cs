using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized;
}

public class GestureDetector : MonoBehaviour
{
    // Start is called before the first frame update
    public OVRSkeleton Lskeleton;
    public OVRSkeleton Rskeleton;
    public List<Gesture> gestures;
    private List<OVRBone> LfingerBones;
    private List<OVRBone> RfingerBones;

    public float threshold;
    private Gesture previousGesture;
    public UnityAction onGestureRecognized;

    public static GestureDetector Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        LfingerBones = new List<OVRBone>(Lskeleton.Bones);
        RfingerBones = new List<OVRBone>(Rskeleton.Bones);
    }

    // Update is called once per frame
    void Update()
    {

        if (LOKgestureRecognize())
        {
            if (ROKgestureRecognize())
            {
#if UNITY_EDITOR

#else
                onGestureRecognized();
#endif
            }
        }
    }

    //public void getGesturPosition()
    //{
    //    //VRDebug.Instance.Clear();
    //    //VRDebug.Instance.Log("SetGesture");
    //    //foreach(var bone in fingerBones)
    //    //{
    //    //    Vector3 bonepos = skeleton.transform.InverseTransformPoint(bone.Transform.position);
    //    //    VRDebug.Instance.Log(bonepos.ToString("F3"));
    //    //}

    //    //VRDebug.Instance.Log("distance:"+gestureRecognize().ToString());
    //    gestureRecognize();
    //}

    bool LOKgestureRecognize()
    {
        //float currentMin = Mathf.Infinity;
        float sumDistance = 0;
        for (int i = 0; i < LfingerBones.Count; i++)
        {
            Vector3 currentData = Lskeleton.transform.InverseTransformPoint(LfingerBones[i].Transform.position);
            float distance = Vector3.Distance(currentData, gestures[0].fingerDatas[i]);
            sumDistance += distance;
        }

        if (sumDistance < threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }

    bool ROKgestureRecognize()
    {
        //float currentMin = Mathf.Infinity;
        float sumDistance = 0;
        for (int i = 0; i < RfingerBones.Count; i++)
        {
            Vector3 currentData = Rskeleton.transform.InverseTransformPoint(RfingerBones[i].Transform.position);
            float distance = Vector3.Distance(currentData, - gestures[0].fingerDatas[i]);
            sumDistance += distance;
        }

        if (sumDistance < threshold)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
