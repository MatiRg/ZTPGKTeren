using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texkst : MonoBehaviour
{
    private Terrain _myTerr;
    private TerrainData _myTerrData;
    private int _xRes;
    private int _yRes;
    public TextAsset imageAsset;

    // Start is called before the first frame update
    void Start()
    {
        _myTerr = GetComponent<Terrain>();
        _myTerrData = _myTerr.terrainData;
        _xRes = _myTerrData.heightmapWidth;
        _yRes = _myTerrData.heightmapHeight;

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageAsset.bytes);

        var _terrHeights = _myTerrData.GetHeights(0, 0, _xRes, _yRes);
        for (int i = 0; i < _xRes; i++)
        {
            for (int j = 0; j < _yRes; j++)
            {
                _terrHeights[i, j] = 0;
            }
        }
        //
        if (_xRes < tex.width || _yRes < tex.height )
        {
            Debug.Log("Invalid size");
        }
        //
        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                int k = tex.width - i - 1;
                Color pixel = tex.GetPixel(k, j);
                if (pixel == Color.white)
                {
                    _terrHeights[i, j] = 0.0f;
                }
                else
                {
                    _terrHeights[i, j] = 0.1f;
                }
            }
        }
        //
        _myTerrData.SetHeights(0, 0, _terrHeights);
    }
}
