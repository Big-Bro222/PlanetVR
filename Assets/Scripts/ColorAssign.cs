using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAssign : MonoBehaviour
{
    [SerializeField] ColourSettings  colorSettings;

    public void AssignColorSettings()
    {
        Planet currentPlanet = UIController.Instance.currentFocusPlanet.GetComponent<Planet>();
        currentPlanet.colorSettings = colorSettings;
        currentPlanet.OnAssignColorSettingPrefab(colorSettings);
    }
}
