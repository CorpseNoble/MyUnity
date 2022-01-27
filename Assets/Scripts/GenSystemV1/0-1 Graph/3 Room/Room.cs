using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.GenSystemV1
{
    public abstract class Room : GraphElement
    {
        public Vector3 interestPlace;


        public void GenRoomEntry()
        {
            if (PrefsGraph.Instant.SettingGraph.DangerSaveRoomPercent.GetValue())
            {
                FabricGameObject.InstantiatePrefab(PrefsGraph.Instant.elementsData.Chest, interestPlace, transform);
                if (PrefsGraph.Instant.SettingGraph.EnemyPercent.GetValue())
                {
                    var spawner = PrefsGraph.Instant.elementsData.MonsterSpawners.First();
                    var pos = PrefsGraph.Instant.SettingGraph.SelectRandElements(1, subElements).First().transform.position;
                    FabricGameObject.InstantiatePrefab(spawner, pos, transform);

                }
                if (PrefsGraph.Instant.SettingGraph.TrapPercent.GetValue())
                {

                }
            }
            else
            {
                FabricGameObject.InstantiatePrefab(PrefsGraph.Instant.elementsData.Bonfire, interestPlace, transform);
            }
        }
    }

}
