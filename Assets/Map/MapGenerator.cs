using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PerlinNoiseMap : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemapBase;
    [SerializeField] private Tilemap _tilemapLayer1;
    private Dictionary<String, Tile> _tiles;

    [SerializeField] private int _width = 100;
    [SerializeField] private int _height = 100;
    
    private const string TileBaseName = "TilesetGraveyard_";
    
    // Start is called before the first frame update
    void Start()
    {
        InitTiles();

        _tiles.TryGetValue(TileBaseName + "54", out var tile);
        if (tile == null) return;

        for (int i = -_width/2; i < _width/2; i++)
        {
            for (int j = -_height/2; j < _height/2; j++)
            {
                _tilemapBase.SetTile(new Vector3Int(j,i,0),tile);
            }
        }
    }

    private void InitTiles()
    {
        var tiles = Resources.LoadAll<Tile>("Tileset/Tiles/");
        
        _tiles = new Dictionary<string, Tile>();
        foreach (var tile in tiles)
        {
            _tiles.Add(tile.name,tile);
        }
        
    }
}
