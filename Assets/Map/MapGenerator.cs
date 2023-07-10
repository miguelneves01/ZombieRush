
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


public class PerlinNoiseMap : MonoBehaviour
{
    [SerializeField] private List<Tilemap> _tilemaps;
    private List<Dictionary<int, TileBase>> _tiles;

    [SerializeField] private int _width = 100;
    [SerializeField] private int _height = 100;
    
    [SerializeField] private float _magnification = 7f;
    [SerializeField] private int _spawnChance = 100;
    [SerializeField] private int _xOffset = 0;
    [SerializeField] private int _yOffset = 0;

    private const string TilesBasePath = "Tileset/Tiles/L";

    // Start is called before the first frame update
    void Start()
    {
        _xOffset = Random.Range(-1000,1000);
        _yOffset = Random.Range(-1000,1000);
        
        InitTiles();
        InitBaseTilemap();

        InitLayer(0,100, _magnification);
        InitLayer(2,_spawnChance, 10f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _xOffset = Random.Range(-1000,1000);
            _yOffset = Random.Range(-1000,1000);
            InitLayer(0, 100, _magnification);
        }
    }

    private void InitTiles()
    {
        _tiles = new List<Dictionary<int, TileBase>>();
        for (int i = 0; i < 3; i++)
        {
            _tiles.Add(new Dictionary<int, TileBase>());
        }

        for (int i = 0; i < 3; i++)
        {
            var tileArray = Resources.LoadAll<TileBase>(TilesBasePath + i + "/");
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

    private void InitLayer(int layer, int spawnChance,float magnitude)
    {
        for (int i = -_width/2; i < _width/2; i++)
        {
            for (int j = -_height/2; j < _height/2; j++)
            {
                var tile = GetTileFromNoise(i, j, layer, spawnChance,magnitude);
                if (tile == null)
                {
                    continue;
                }
                
                _tilemaps[layer + 1].SetTile(new Vector3Int(i,j,0),tile);
            }
        }
    }

    private TileBase GetTileFromNoise(int x, int y, int layer, int spawnChance, float magnitude)
    {
        // float spawn = Random.Range(0f, 100f);
        // if (spawn < spawnChance)
        // {
        //     return null;
        // }
        
        float perlin = Mathf.Clamp01(Mathf.PerlinNoise(
            (x - _xOffset) / magnitude, 
            (y - _yOffset) / magnitude
            ));
        int tilesAmt = _tiles[layer].Count;
        int scaledPerlin = Mathf.FloorToInt(perlin * tilesAmt);

        _tiles[layer].TryGetValue(scaledPerlin,out TileBase tile);
        return tile;
    }

}
