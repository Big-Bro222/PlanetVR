using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Ismoveable=false;
    public float Radius;
    public float Constantfloat;
    public Vector3 RotationWorldAxis;
    public Vector3 StartPoint;
    void Start()
    {
        Constantfloat = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Ismoveable)
        {
            transform.RotateAround(UIController.Instance.OrbitCenter.transform.position, RotationWorldAxis, (Constantfloat / Radius) * Time.deltaTime);
        }
    }


}
