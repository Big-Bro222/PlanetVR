using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator:MonoBehaviour {

    [SerializeField]public ShapeSettings settings;
    [HideInInspector]public NoiseLayer[] noiseLayers;



    //public MinMax elevationMinMax;

    private void Awake()
    {

        noiseLayers=settings.noiseLayers;
    }

    //public void UpdateSettings(ShapeSettings settings)
    //{
    //    this.settings = settings;
    //    if (noiseFilters == null)
    //    {
    //        noiseFilters = new NoiseFilter[settings.noiseLayers.Length];
    //        for (int i = 0; i < noiseFilters.Length; i++)
    //        {
    //            noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
    //        }
    //        elevationMinMax = new MinMax();
    //    }
    //}

    //public void UpdateSettings()
    //{
    //    this.settings = settings;
    //    if (noiseFilters == null)
    //    {
    //        noiseFilters = new NoiseFilter[settings.noiseLayers.Length];
    //        for (int i = 0; i < noiseFilters.Length; i++)
    //        {
    //            noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
    //        }
    //        elevationMinMax = new MinMax();
    //    }
    //}

    //public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    //{
    //    if (noiseFilters == null)
    //    {
    //        noiseFilters = new NoiseFilter[settings.noiseLayers.Length];
    //        for (int i = 0; i < noiseFilters.Length; i++)
    //        {
    //            noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
    //        }
    //        elevationMinMax = new MinMax();
    //    }

    //    float firstLayerValue = 0;
    //    float elevation = 0;

    //    if (noiseFilters.Length > 0)
    //    {
    //        firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
    //        if (settings.noiseLayers[0].enabled)
    //        {
    //            elevation = firstLayerValue;
    //        }
    //    }

    //    for (int i = 1; i < noiseFilters.Length; i++)
    //    {
    //        if (settings.noiseLayers[i].enabled)
    //        {
    //            float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
    //            elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
    //        }
    //    }
    //    elevation = settings.planetRadius * (1 + elevation);
    //    elevationMinMax.AddValue(elevation);
    //    return pointOnUnitSphere *elevation;
    //}
}
