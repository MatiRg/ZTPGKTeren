using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//! Sample terrain animator/generator
public class TerrainGeneration1: MonoBehaviour 
{
    private Terrain _myTerr;
    private TerrainData _myTerrData;
    private int _xRes;
    private int _yRes;
    float[,] _terrHeights;
    float[,] originalTerrainSectionHeight;
    public int radiusOfAnimation = 80;

    // Use this for initialization
    void Start()
    {
        // Get terrain and terrain data handles
        _myTerr = GetComponent<Terrain>();
        _myTerrData = _myTerr.terrainData;
        // Get terrain dimensions in tiles (X tiles x Y tiles)
        _xRes = _myTerrData.heightmapWidth;
        _yRes = _myTerrData.heightmapHeight;
        // Set heightmap
        RandomizeTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        // Call animation function
        AnimTerrain ();
    }

    // Set the terrain using noise pattern
    private void RandomizeTerrain()
    {
        // Extract entire heightmap (expensive!)
        _terrHeights = _myTerrData.GetHeights(0, 0, _xRes, _yRes);
        // STUDENT'S CODE //
        int octaves = 3;
        float[] scale = new float[octaves];
        for (int k = 0; k < octaves; k++)
        {
            scale[k] = UnityEngine.Random.Range(1.0f, 2.5f);
        }
        for (int i = 0; i < _xRes; i++)
        {
            for (int j = 0; j < _yRes; j++)
            {
                float xCoeff = (float)i / _xRes;
                float yCoeff = (float)j / _yRes;
                _terrHeights[i, j] = 0;
                for (int k = 0; k < octaves; k++)
                {
                    _terrHeights[i, j] += Mathf.PerlinNoise(xCoeff * scale[k], yCoeff * scale[k])*0.25f;
                }
                _terrHeights[i, j] /= (float)octaves;
            }
        }
        // Set entire heightmap (expensive!)
        _myTerrData.SetHeights (0, 0, _terrHeights);
        //
        originalTerrainSectionHeight = _myTerrData.GetHeights(_xRes/2, _yRes/2, radiusOfAnimation*2, radiusOfAnimation*2);
    }

    // Animate part of the terrain
    private void AnimTerrain()
    {
        _terrHeights = _myTerrData.GetHeights(_xRes/2, _yRes/2, radiusOfAnimation*2, radiusOfAnimation*2);
        for(int i=0; i<radiusOfAnimation*2; i++) 
        {
            for(int j=0; j<radiusOfAnimation*2; j++) 
            {
                _terrHeights[i, j] = (float)Math.Cos(Time.time) * originalTerrainSectionHeight[i, j];
            }
        }
        _myTerrData.SetHeights(_xRes / 2, _yRes / 2, _terrHeights);
    }
}
