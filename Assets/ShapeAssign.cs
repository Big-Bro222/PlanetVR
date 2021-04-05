using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeAssign : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ShapeSettings shapeSettings;
    public void AssignShapeSettings()
    {
        VRDebug.Instance.Log("AssignShape");
        Planet currentPlanet = UIController.Instance.currentFocusPlanet.GetComponent<Planet>();
        currentPlanet.shapeSettings = shapeSettings;
        currentPlanet.OnAssignShapeSettingPrefab(shapeSettings);
    }


}
