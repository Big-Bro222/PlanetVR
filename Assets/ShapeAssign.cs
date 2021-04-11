using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeAssign : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ShapeSettings shapeSettings;
    public void AssignShapeSettings()
    {
        Planet currentPlanet = UIController.Instance.currentFocusPlanet.GetComponent<Planet>();
        Debug.Log("assign");
        currentPlanet.shapeSettings = shapeSettings;
        currentPlanet.OnAssignShapeSettingPrefab(shapeSettings);
    }

    private void OnMouseDown()
    {
        AssignShapeSettings();
    }

}
