using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Planet : MonoBehaviour
{
    [Range(4, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;
    public MinMax elevationMinMax;

    [SerializeField,HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
    NoiseFilter[] noiseFilters;
    [SerializeField] float planetRadius;
    [SerializeField] PlanetNoiseLayer[] noiseLayers;
    [SerializeField] Gradient gradient;
    [SerializeField] ShapeGenerator shapeGenerator;
    [SerializeField] ColourGenerator colourGenerator;

    SphereCollider sphereCollider;
    Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
    const int textureResolution = 50;
    Material planetMaterial;
    Texture2D texture;





    ColourSettings colourSettings;
    private void Start()
    {
        //colourSettings = colourGenerator.settings;
        //colourGenerator.UpdateSettings();

        //Synchronize the color information of the globel at the beginning of the project.
        gradient = colourGenerator.settings.gradient;
        planetMaterial=colourGenerator.settings.planetMaterial;
        texture = new Texture2D(textureResolution, 1);

        //Synchronize the shape information of the globel at the beginning of the project.
        noiseLayers = new PlanetNoiseLayer[shapeGenerator.noiseLayers.Length];
        for(int i = 0; i < noiseLayers.Length; i++)
        {
            noiseLayers[i] = new PlanetNoiseLayer();
            noiseLayers[i].enabled = true;
            noiseLayers[i].useFirstLayerAsMask = shapeGenerator.noiseLayers[i].useFirstLayerAsMask;
            noiseLayers[i].noiseType = shapeGenerator.noiseLayers[i].noiseSettings.noiseType;
            noiseLayers[i].strength = shapeGenerator.noiseLayers[i].noiseSettings.strength;
            noiseLayers[i].numLayers = shapeGenerator.noiseLayers[i].noiseSettings.numLayers;
            noiseLayers[i].baseRoughness = shapeGenerator.noiseLayers[i].noiseSettings.baseRoughness;
            noiseLayers[i].roughness = shapeGenerator.noiseLayers[i].noiseSettings.roughness;
            noiseLayers[i].persistence = shapeGenerator.noiseLayers[i].noiseSettings.persistence;
            noiseLayers[i].centre= shapeGenerator.noiseLayers[i].noiseSettings.centre;
            noiseLayers[i].minValue= shapeGenerator.noiseLayers[i].noiseSettings.minValue;

            noiseLayers[i].weightMultiplier = .8f;
        }

        planetRadius = shapeGenerator.settings.planetRadius;


        elevationMinMax = new MinMax();
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = planetRadius;
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];



        CalculateVectors();
        GenerateColours();
        GenerateMesh();

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

    private void Update()
    {
        //each time the shapesetting changes, run this code
        //todo: link with shapesettings.
        //CalculateVectors();
        //GenerateColours();
        //GenerateMesh();
    }

    public void GenerateNewSettings()
    {
        CalculateVectors();
        GenerateColours();
        GenerateMesh();
    }

    void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
        UpdateElevation(elevationMinMax);
        sphereCollider.radius = planetRadius;

    }

    void GenerateColours()
    {
        UpdateColours();
    }


    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            CalculateVectors();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated()
    {
        if (autoUpdate)
        {
            CalculateVectors();
            GenerateColours();
        }
    }
    public void UpdateElevation(MinMax elevationMinMax)
    {
        planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public void UpdateColours()
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

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        //if (noiseFilters == null||noiseFilters.Length!=noiseLayers.Length)
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
            elevationMinMax = new MinMax();
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


