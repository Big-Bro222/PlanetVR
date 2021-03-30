using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MinMax {

    //public float Min { get; private set; }
    public float Min;

    //public float Max { get; private set; }
    public float Max;

    public MinMax()
    {
        Min = float.MaxValue;
        Max = float.MinValue;
    }

    public void AddValue(float v)
    {
        if (v > Max)
        {
            Max = v;
        }
        if (v < Min)
        {
            Min = v;
        }
    }
}
