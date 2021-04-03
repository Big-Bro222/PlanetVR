using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeAssign : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ShapeGenerator shapeGenerator;
    public void AssignShapeSettings()
    {
        Planet currentPlanet = UIController.Instance.currentFocusPlanet.GetComponent<Planet>();
        currentPlanet.shapeSettings = shapeGenerator.settings;
        currentPlanet.OnAssignShapeSettingPrefab(shapeGenerator.settings);
        VRDebug.Instance.Log("Assign");
    }


}
