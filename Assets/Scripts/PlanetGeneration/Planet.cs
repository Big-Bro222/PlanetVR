using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[System.Serializable]
public class Planet : MonoBehaviour
{
    [Range(4, 256)]
    public int resolution = 10;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };

    public enum TerrainType { Land,Moutain};
    public enum ParameterType { Strength,Roughness, Centre, Minvalue, Weight, Persistence } 


    public FaceRenderMask faceRenderMask;
    public MinMax elevationMinMax;

    [SerializeField,HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
    NoiseFilter[] noiseFilters;
    [SerializeField] float planetRadius;
    [SerializeField] PlanetNoiseLayer[] noiseLayers;
    [SerializeField] Gradient gradient;
    //[SerializeField] public ShapeGenerator shapeGenerator;
    //[SerializeField] public ColourGenerator colourGenerator;


    [SerializeField] public ShapeSettings shapeSettings;
    [SerializeField] public ColourSettings colorSettings;


    SphereCollider sphereCollider;
    Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
    const int textureResolution = 50;
    Material planetMaterial;
    Texture2D texture;


    private void Awake()
    {
        Debug.Log(name);
        elevationMinMax = new MinMax();
        sphereCollider = GetComponent<SphereCollider>();

        meshFilters = new MeshFilter[6];
        terrainFaces = new TerrainFace[6];

        PresetColor();
        PresetShape();
    }

    public void Start()
    {
        CalculateVectors();
        GenerateMesh();
        GenerateColours();
    }

    private void Update()
    {
    }
    private void PresetShape()
    {
        //Synchronize the shape information of the globel at the beginning of the project.
        noiseLayers = new PlanetNoiseLayer[shapeSettings.noiseLayers.Length];
        for (int i = 0; i < noiseLayers.Length; i++)
        {
            noiseLayers[i] = new PlanetNoiseLayer();
            noiseLayers[i].enabled = true;
            noiseLayers[i].useFirstLayerAsMask = shapeSettings.noiseLayers[i].useFirstLayerAsMask;
            noiseLayers[i].noiseType = shapeSettings.noiseLayers[i].noiseSettings.noiseType;
            noiseLayers[i].strength = shapeSettings.noiseLayers[i].noiseSettings.strength;
            noiseLayers[i].numLayers = shapeSettings.noiseLayers[i].noiseSettings.numLayers;
            noiseLayers[i].baseRoughness = shapeSettings.noiseLayers[i].noiseSettings.baseRoughness;
            noiseLayers[i].roughness = shapeSettings.noiseLayers[i].noiseSettings.roughness;
            noiseLayers[i].persistence = shapeSettings.noiseLayers[i].noiseSettings.persistence;
            noiseLayers[i].centre = shapeSettings.noiseLayers[i].noiseSettings.centre;
            noiseLayers[i].minValue = shapeSettings.noiseLayers[i].noiseSettings.minValue;

            noiseLayers[i].weightMultiplier = .8f;
        }

        planetRadius = shapeSettings.planetRadius;

        sphereCollider.radius = planetRadius;

        
    }

    private void PresetColor()
    {

        //Synchronize the color information of the globel at the beginning of the project.
        gradient = colorSettings.gradient;
        planetMaterial = colorSettings.planetMaterial;
        texture = new Texture2D(textureResolution, 1);
    }

    private void CalculateVectors()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.transform.localPosition = Vector3.zero;
                meshObj.transform.localRotation = Quaternion.identity;


                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = planetMaterial;

            terrainFaces[i] = new TerrainFace(this, meshFilters[i].sharedMesh, resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }


    public void GenerateNewSettings()
    {
        CalculateVectors();
        GenerateColours();
        GenerateMesh();
    }

    public void OnAssignShapeSettingPrefab(ShapeSettings shapeSettings)
    {
        //if assign a brand new shape preset generator;
        this.shapeSettings = shapeSettings;
        PresetShape();
        CalculateVectors();
        GenerateMesh();
    }

    public void OnAssignColorSettingPrefab(ColourSettings colourSettings)
    {
        //if assign a brand new color preset generator;
        //this.colourGenerator = colourGenerator;
        this.colorSettings = colourSettings;
        PresetColor();
        CalculateVectors();
        GenerateColours();
    }
    //update shapesettings
    public void OnShapeSettingsUpdated(TerrainType terrainType, ParameterType parameterType, float parameter)
    {
        Debug.Log("Update");
        int layerIndex = (int)terrainType;
        switch (parameterType)
        {
            case ParameterType.Strength:
                noiseLayers[layerIndex].strength=parameter;
                break;
            case ParameterType.Persistence:
                noiseLayers[layerIndex].persistence = parameter;
                break;
            case ParameterType.Roughness:
                noiseLayers[layerIndex].roughness = parameter;
                break;
            case ParameterType.Minvalue:
                noiseLayers[layerIndex].minValue = parameter;
                break;
            case ParameterType.Weight:
                noiseLayers[layerIndex].weightMultiplier = parameter;
                break;
            default:
                Debug.LogError("Undefined parameter");
                break;
        }

        //Thread safety
            CalculateVectors();
            GenerateMesh();

    }
    //update resolution
    public void OnShapeSettingsUpdated(int resolution)
    {
        this.resolution = resolution;
        CalculateVectors();
        GenerateMesh();
    }
    //update radius
    public void OnShapeSettingsUpdated(float radius)
    {
        planetRadius = radius;
        CalculateVectors();
        GenerateMesh();
    }
    //update center position
    public void OnShapeSettingsUpdated(TerrainType terrainType, Vector3 centre)
    {
        if (terrainType == TerrainType.Land)
        {
            noiseLayers[0].centre = centre;
        }
        else if(terrainType == TerrainType.Moutain)
        {
            noiseLayers[1].centre = centre;
        }
        else
        {
            Debug.LogError("Cannot find the TerrainType");
        }
        CalculateVectors();
        GenerateMesh();
    }


    public int GetShpaeSettingintPara()
    {
        return resolution;
    }
    public float GetShapeSettingfloatPara()
    {

        return planetRadius;
    }
    public float GetShapeSettingPara(TerrainType terrainType, ParameterType parameterType)
    {

        int layerIndex = (int)terrainType;
        switch (parameterType)
        {
            case ParameterType.Strength:
                return noiseLayers[layerIndex].strength;
            case ParameterType.Persistence:
                return noiseLayers[layerIndex].persistence;
            case ParameterType.Roughness:
                return noiseLayers[layerIndex].roughness;
            case ParameterType.Minvalue:
                return noiseLayers[layerIndex].minValue;
            case ParameterType.Weight:
                return noiseLayers[layerIndex].weightMultiplier;
            default:
                Debug.LogError("Undefined parameter");
                return 0;
        }
    }

    public void OnColourSettingsUpdated()
    {
            CalculateVectors();
            GenerateColours();
    }


    private void GenerateColours()
    {
        UpdateColours();
    }
    private void UpdateElevation(MinMax elevationMinMax)
    {
        planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }
    private void UpdateColours()
    {
        Color[] colours = new Color[textureResolution];
        for (int i = 0; i < textureResolution; i++)
        {
            colours[i] = gradient.Evaluate(i / (textureResolution - 1f));
        }
        texture.SetPixels(colours);
        texture.Apply();
        planetMaterial.SetTexture("_texture", texture);
    }
    private void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
        UpdateElevation(elevationMinMax);
        planetRadius = shapeSettings.planetRadius;
        sphereCollider.radius = planetRadius;


    }
    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        if (true)
            {
                noiseFilters = new NoiseFilter[noiseLayers.Length];
            for (int i = 0; i < noiseFilters.Length; i++)
            {
                noiseFilters[i] = new NoiseFilter(noiseLayers[i].noiseType,
                noiseLayers[i].strength,
                noiseLayers[i].numLayers,
                noiseLayers[i].baseRoughness,
                noiseLayers[i].roughness,
                noiseLayers[i].persistence,
                noiseLayers[i].centre,
                noiseLayers[i].minValue,
                noiseLayers[i].weightMultiplier);
            }
            //elevationMinMax = new MinMax();
        }

        float firstLayerValue = 0;
        float elevation = 0;

        if (noiseFilters.Length > 0)
        {
            firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
            if (noiseLayers[0].enabled)
            {
                elevation = firstLayerValue;
            }
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (noiseLayers[i].enabled)
            {
                float mask = (noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }
        elevation = planetRadius * (1 + elevation);
        elevationMinMax.AddValue(elevation);
        return pointOnUnitSphere * elevation;
    }


}


