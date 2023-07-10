using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private int _spawnAmount;
    [SerializeField] private Vector2 _xSpawnRange;
    [SerializeField] private Vector2 _ySpawnRange;
    [SerializeField] private Vector2 _minSpawnRange;
    [SerializeField] private List<GameObject> _enemies;

    private float _curTime;

    // Update is called once per frame
    void Update()
    {
        _curTime += Time.deltaTime;
        if (_curTime < _spawnRate)return;
        
        for (int i = 0; i < _spawnAmount; i++)
        {
            var enemy = _enemies[Random.Range(0,_enemies.Count)];
            float spawnX = Random.Range(_xSpawnRange.x,_xSpawnRange.y);
            float spawnY = Random.Range(_ySpawnRange.x,_ySpawnRange.y);

            if (Mathf.Abs(spawnX) < _minSpawnRange.x)
            {
                spawnX = spawnX > 0 ? spawnX + _minSpawnRange.x : spawnX - _minSpawnRange.x;
            }
            
            if (Mathf.Abs(spawnY) < _minSpawnRange.y)
            {
                spawnY = spawnY > 0 ? spawnY + _minSpawnRange.y : spawnY - _minSpawnRange.y;
            }

            Vector3 pos = transform.position + new Vector3(spawnX,spawnY,0);

            Instantiate(enemy, pos, Quaternion.identity);

            _curTime = 0;
        }
        
    }
}
