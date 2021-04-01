using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public enum UIstate { General, Selecting ,ShapeSettings ,ColorSettings}
    [SerializeField] GameObject presetColorSettings;
    [SerializeField] GameObject presetShapeSettings;
    [SerializeField] GameObject galaxyOrbit;
    [SerializeField] GameObject generalInstruction;
    [SerializeField] GameObject settingsInstruction;
    [SerializeField] GameObject prefabMenu;
    [SerializeField] GameObject shapeDetailMenu;
    [SerializeField] GameObject colorDetailMenu;

    public GameObject ReviewPoint;
    public GameObject sheild;


    public GameObject currentFocusPlanet;
    public GameObject OrbitPoint;
    
    public UIstate currentState;

    public static UIController Instance { get; private set; }
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
        currentState = UIstate.General;

    }

    public void EnterUIState(UIstate newUiState)
    {
        if (newUiState == currentState)
        {
            return;
        }
        ExitUIState(currentState);

        currentState = newUiState;
        //ToDo: setup new state

        switch (newUiState)
        {
            case UIstate.General:
                generalInstruction.SetActive(true);
                galaxyOrbit.SetActive(true);
                prefabMenu.SetActive(true);
                break;
            case UIstate.Selecting:
                generalInstruction.SetActive(true);
                galaxyOrbit.SetActive(true);
                prefabMenu.SetActive(true);
                //Todo: change the color alpha gradually.
                sheild.SetActive(true);
                break;
            case UIstate.ShapeSettings:
                settingsInstruction.SetActive(true);
                presetShapeSettings.SetActive(true);
                shapeDetailMenu.SetActive(true);
                break;
            case UIstate.ColorSettings:
                settingsInstruction.SetActive(true);
                presetColorSettings.SetActive(true);
                colorDetailMenu.SetActive(true);
                break;
            default:
                break;
        }

    }


    private void ExitUIState(UIstate oldUIState)
    {
        //ToDo: reset old state;
        switch (oldUIState)
        {
            case UIstate.General:
                generalInstruction.SetActive(false);
                galaxyOrbit.SetActive(false);
                prefabMenu.SetActive(false);
                break;
            case UIstate.Selecting:
                generalInstruction.SetActive(false);
                galaxyOrbit.SetActive(false);
                prefabMenu.SetActive(false);
                //Todo: change the color alpha gradually.
                sheild.SetActive(false);
                break;
            case UIstate.ShapeSettings:
                settingsInstruction.SetActive(false);
                presetShapeSettings.SetActive(false);
                shapeDetailMenu.SetActive(false);
                break;
            case UIstate.ColorSettings:
                settingsInstruction.SetActive(false);
                presetColorSettings.SetActive(false);
                colorDetailMenu.SetActive(false);
                break;
            default:
                break;
        }

    }
}
