using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class SpawnedObject : MonoBehaviour
    {
        public Spawner spawner;

        private void OnDestroy()
        {
            spawner?.spawnedObjects.Remove(this);
        }
    }
}
