using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


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

    //slider settings
    [SerializeField] VRSlider Resolution;
    [SerializeField] VRSlider Radius;
    [SerializeField] VRSlider land_Strength;
    [SerializeField] VRSlider land_Roughness;
    [SerializeField] VRSlider land_Persistence;
    [SerializeField] VRSlider land_MinValue;
    [SerializeField] VRSlider moutain_Strength;
    [SerializeField] VRSlider moutain_Roughness;
    [SerializeField] VRSlider moutain_Persistence;
    [SerializeField] VRSlider moutain_MinValue;
    [SerializeField] VRSlider moutain_Weight;



    public GameObject ReviewPoint;
    public GameObject sheild;


    public GameObject currentFocusPlanet;
    public GameObject OrbitPoint;
    public GameObject OrbitFolder;
    
    public UIstate currentState;

    public static UIController Instance { get; private set; }

    Sequence s1;
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
        s1 = DOTween.Sequence();

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
                SubscribeShapeSettings();
                GestureDetector.Instance.onGestureRecognized+= Togeneral;
                break;
            case UIstate.ColorSettings:
                settingsInstruction.SetActive(true);
                presetColorSettings.SetActive(true);
                colorDetailMenu.SetActive(true);
                SubscribeColorSettings();
                GestureDetector.Instance.onGestureRecognized += Togeneral;

                break;
            default:
                break;
        }

    }


    public void ExitUIState(UIstate oldUIState)
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
                UnsubscribeShapeSettings();
                GestureDetector.Instance.onGestureRecognized -= Togeneral;

                break;
            case UIstate.ColorSettings:
                settingsInstruction.SetActive(false);
                presetColorSettings.SetActive(false);
                colorDetailMenu.SetActive(false);
                UnsubscribeColorSettings();
                GestureDetector.Instance.onGestureRecognized -= Togeneral;

                break;
            default:
                break;
        }

    }

    private void SubscribeShapeSettings()
    {
        Resolution.onsliderChange += (value)=>currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated((int)value);
        Radius.onsliderChange += (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(value);

        land_Strength.onsliderChange += (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Land, Planet.ParameterType.Strength, value);
        land_Roughness.onsliderChange += (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Land, Planet.ParameterType.Roughness, value);
        land_Persistence.onsliderChange += (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Land, Planet.ParameterType.Persistence, value);
        land_MinValue.onsliderChange += (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Land, Planet.ParameterType.Minvalue, value);
        moutain_Strength.onsliderChange += (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Moutain, Planet.ParameterType.Strength, value);
        moutain_Roughness.onsliderChange += (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Moutain, Planet.ParameterType.Roughness, value);
        moutain_Persistence.onsliderChange += (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Moutain, Planet.ParameterType.Persistence, value);
        moutain_MinValue.onsliderChange += (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Moutain, Planet.ParameterType.Minvalue, value);
        moutain_Weight.onsliderChange += (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Moutain, Planet.ParameterType.Weight, value);
        setupShapeSettingstoSlider(true);
    }

    private void UnsubscribeShapeSettings()
    {
        Resolution.onsliderChange -= (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated((int)value);
        Radius.onsliderChange -= (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(value);

        land_Strength.onsliderChange -= (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Land, Planet.ParameterType.Strength, value);
        land_Roughness.onsliderChange -= (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Land, Planet.ParameterType.Roughness, value);
        land_Persistence.onsliderChange -= (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Land, Planet.ParameterType.Persistence, value);
        land_MinValue.onsliderChange -= (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Land, Planet.ParameterType.Minvalue, value);
        moutain_Strength.onsliderChange -= (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Moutain, Planet.ParameterType.Strength, value);
        moutain_Roughness.onsliderChange -= (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Moutain, Planet.ParameterType.Roughness, value);
        moutain_Persistence.onsliderChange -= (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Moutain, Planet.ParameterType.Persistence, value);
        moutain_MinValue.onsliderChange -= (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Moutain, Planet.ParameterType.Minvalue, value);
        moutain_Weight.onsliderChange -= (value) => currentFocusPlanet.GetComponent<Planet>().OnShapeSettingsUpdated(Planet.TerrainType.Moutain, Planet.ParameterType.Weight, value);
        setupShapeSettingstoSlider(false);

    }

    public void setupShapeSettingstoSlider(bool isEnter)
    {
        if (isEnter)
        {
            Debug.Log(currentFocusPlanet.name);
            Resolution.SetValue(currentFocusPlanet.GetComponent<Planet>().GetShpaeSettingintPara());
            Radius.SetValue(currentFocusPlanet.GetComponent<Planet>().GetShapeSettingfloatPara());
            land_Strength.SetValue(currentFocusPlanet.GetComponent<Planet>().GetShapeSettingPara(Planet.TerrainType.Land, Planet.ParameterType.Strength));
            land_Roughness.SetValue(currentFocusPlanet.GetComponent<Planet>().GetShapeSettingPara(Planet.TerrainType.Land, Planet.ParameterType.Roughness));
            land_Persistence.SetValue(currentFocusPlanet.GetComponent<Planet>().GetShapeSettingPara(Planet.TerrainType.Land, Planet.ParameterType.Persistence));
            land_MinValue.SetValue(currentFocusPlanet.GetComponent<Planet>().GetShapeSettingPara(Planet.TerrainType.Land, Planet.ParameterType.Minvalue));
            moutain_Strength.SetValue(currentFocusPlanet.GetComponent<Planet>().GetShapeSettingPara(Planet.TerrainType.Moutain, Planet.ParameterType.Strength));
            moutain_Roughness.SetValue(currentFocusPlanet.GetComponent<Planet>().GetShapeSettingPara(Planet.TerrainType.Moutain, Planet.ParameterType.Roughness));
            moutain_Persistence.SetValue(currentFocusPlanet.GetComponent<Planet>().GetShapeSettingPara(Planet.TerrainType.Moutain, Planet.ParameterType.Persistence));
            moutain_MinValue.SetValue(currentFocusPlanet.GetComponent<Planet>().GetShapeSettingPara(Planet.TerrainType.Moutain, Planet.ParameterType.Minvalue));
            moutain_Weight.SetValue(currentFocusPlanet.GetComponent<Planet>().GetShapeSettingPara(Planet.TerrainType.Moutain, Planet.ParameterType.Weight));
        }
        else
        {
            Resolution.SetValue(0);
            Radius.SetValue(0);
            land_Strength.SetValue(0);
            land_Roughness.SetValue(0);
            land_Persistence.SetValue(0);
            land_MinValue.SetValue(0);
            moutain_Strength.SetValue(0);
            moutain_Roughness.SetValue(0);
            moutain_Persistence.SetValue(0);
            moutain_MinValue.SetValue(0);
            moutain_Weight.SetValue(0);
        }
    }

    private void UnsubscribeColorSettings()
    {

    }
    private void SubscribeColorSettings()
    {

    }

    public void UpdateShapeSettingsUI()
    {

    }

    public void UpdateColorSettingsUI()
    {

    }


    public void switchtosettings(bool isToShapeSettings)
    {
        if (isToShapeSettings)
        {
            EnterUIState(UIstate.ShapeSettings);
        }
        else
        {
            EnterUIState(UIstate.ColorSettings);
        }
    }

    public void Togeneral()
    {
        EnterUIState(UIstate.General);
        DOTween.Sequence()
            .Append(currentFocusPlanet.transform.DOMove(UIController.Instance.OrbitPoint.transform.position, 2f).SetEase(Ease.InOutCubic))
            .Join(currentFocusPlanet.transform.DOScale(Vector3.one*1.5f, 3f).SetEase(Ease.InOutCubic))
            .OnComplete(() => {
                currentFocusPlanet.GetComponent<CubeDebugger>().enabled = true;
                currentFocusPlanet = null;
            });


    }

}
