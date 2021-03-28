using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class NoiseFilter {

    //NoiseSettings settings;
    Noise noise = new Noise();

    NoiseSettings.NoiseType noiseType;
    float strength ;
    int numLayers;
    float baseRoughness;
    float roughness;
    float persistence;
    Vector3 centre;
    float minValue;

    float weightMultiplier = .8f;

    public NoiseFilter(NoiseSettings.NoiseType noiseType, float strength, int numLayers,float baseRoughness, float roughness, float persistence, Vector3 centre, float minValue,float weightMultiplier)
    {
        this.noiseType = noiseType;
        this.strength = strength;
        this.numLayers = numLayers;
        this.noiseType = noiseType;
        this.baseRoughness = baseRoughness;
        this.roughness = roughness;
        this.persistence = persistence;
        this.centre = centre;
        this.minValue = minValue;
        this.weightMultiplier = weightMultiplier;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = baseRoughness;
        float amplitude = 1;
        if (noiseType == NoiseSettings.NoiseType.Simple)
        {
            for (int i = 0; i < numLayers; i++)
            {
                float v = noise.Evaluate(point * frequency + centre);
                noiseValue += (v + 1) * .5f * amplitude;
                frequency *= roughness;
                amplitude *= persistence;
            }
        }
        else if(noiseType == NoiseSettings.NoiseType.Rigid)
        {
            float weight = 1;

            for (int i = 0; i < numLayers; i++)
            {
                float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + centre));
                v *= v;
                v *= weight;
                weight = Mathf.Clamp01(v *weightMultiplier);
                noiseValue += v * amplitude;
                frequency *= roughness;
                amplitude *= persistence;
            }
        }

        noiseValue = Mathf.Max(0, noiseValue - minValue);
        return noiseValue * strength;


    }
}
