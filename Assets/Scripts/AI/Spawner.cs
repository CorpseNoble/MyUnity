using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField][Range(0,10)]
    private int _spawnRange = 5;
    [SerializeField][Range(0,10)]
    private int _spawnLimit = 5;
    [SerializeField][Range(0,120)]
    private float _spawnTime = 60;

    private System.Random _random = new System.Random();
    [SerializeField] 
    private List<GameObject> _spawnings = new List<GameObject>();  
    [SerializeField] 
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    
    private void Start()
    {
        StartCoroutine(StartSpawnCoroutine(_spawnTime));
    }

    private IEnumerator StartSpawnCoroutine(float time)
    {
        while (true)
        {

            yield return new WaitForSeconds(time);
            if (_spawnedObjects.Count<_spawnLimit)
                Spawn();
        }
    }

    private void Spawn()
    {
        foreach (var spawning in _spawnings)
        {
            Vector3 randomPosition = new Vector3(transform.position.x + _random.Next(-1 * _spawnRange, _spawnRange),
                transform.position.y + _random.Next(-1 * _spawnRange, _spawnRange));
            Instantiate(spawning, randomPosition, transform.rotation);
        }
    }
}
