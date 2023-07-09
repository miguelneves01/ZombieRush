using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = Unity.Mathematics.Random;

public class PerlinNoiseMap : MonoBehaviour
{
    [SerializeField] private List<Tilemap> _tilemaps;
    private List<Dictionary<int, Tile>> _tiles;

    [SerializeField] private int _width = 100;
    [SerializeField] private int _height = 100;
    
    [SerializeField] private float _magnification = 7f;
    [SerializeField] private int _xOffset = 0;
    [SerializeField] private int _yOffset = 0;

    private const string TilesBasePath = "Tileset/Tiles/L";
    
    // Start is called before the first frame update
    void Start()
    {
        InitTiles();
        InitBaseTilemap();

        InitLayer(0);
    }

    private void InitTiles()
    {
        _tiles = new List<Dictionary<int, Tile>>();
        for (int i = 0; i < 3; i++)
        {
            _tiles.Add(new Dictionary<int, Tile>());
        }

        for (int i = 0; i < 3; i++)
        {
            var tileArray = Resources.LoadAll<Tile>(TilesBasePath + i + "/");
            for (int j = 0; j < tileArray.Length; j++)
            {
                _tiles[i].Add(j, tileArray[j]);
            }
        }
    }

    private void InitBaseTilemap()
    {
        _tiles[0].TryGetValue(0, out var tile);
        if (tile == null) return;

        for (int i = -_width/2; i < _width/2; i++)
        {
            for (int j = -_height/2; j < _height/2; j++)
            {
                _tilemaps[0].SetTile(new Vector3Int(j,i,0),tile);
            }
        }
    }

    private void InitLayer(int layer)
    {
        for (int i = -_width/2; i < _width/2; i++)
        {
            for (int j = -_height/2; j < _height/2; j++)
            {
                var tile = GetTileFromNoise(i, j, layer);
                if (tile == null)
                {
                    return;
                }
                
                _tilemaps[layer + 1].SetTile(new Vector3Int(i,j,0),tile);
            }
        }
    }

    private Tile GetTileFromNoise(int x, int y, int layer)
    {
        float perlin = Mathf.Clamp01(Mathf.PerlinNoise(
            (x - _xOffset) / _magnification, 
            (y - _yOffset) / _magnification
            ));
        int tilesAmt = _tiles[layer].Count;
        int scaledPerlin = Mathf.FloorToInt(perlin * tilesAmt * 2);

        _tiles[layer].TryGetValue(scaledPerlin,out Tile tile);
        return tile;
    }

}
