using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class Spawner : MonoBehaviour
    {
        public List<SpawnedObject> spawnedObjects = new List<SpawnedObject>();

        [SerializeField]
        [Range(0, 10)]
        private int _spawnRange = 5;
        [SerializeField]
        [Range(0, 10)]
        private int _spawnLimit = 5;
        [SerializeField]
        [Range(0, 120)]
        private float _spawnTime = 60;

        private System.Random _random = new System.Random();
        [SerializeField]
        private List<GameObject> _spawnings = new List<GameObject>();


        private void Start()
        {
            //StartCoroutine(StartSpawnCoroutine(_spawnTime));
        }

        private IEnumerator StartSpawnCoroutine(float time)
        {
            while (true)
            {
                if (spawnedObjects.Count < _spawnLimit)
                    Spawn();
                yield return new WaitForSeconds(time);

            }
        }

        private void Spawn()
        {
            foreach (var spawning in _spawnings)
            {
                Vector3 randomPosition = new Vector3(transform.position.x + _random.Next(-1 * _spawnRange, _spawnRange),
                    transform.position.y + _random.Next(-1 * _spawnRange, _spawnRange));
                var spawnedObject = Instantiate(spawning, randomPosition, transform.rotation).AddComponent<SpawnedObject>();
                spawnedObject.spawner = this;
                spawnedObjects.Add(spawnedObject);
            }
        }
    }
}
