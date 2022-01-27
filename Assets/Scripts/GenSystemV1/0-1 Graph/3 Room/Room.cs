using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.GenSystemV1
{
    public abstract class Room : GraphElement
    {
        [Header("Room")]
        public Point interestPlace;
        public int countSpawners = 1;

        public void GenRoomEntry()
        {
            if (interestPlace != null)
                if (PrefsGraph.Instant.SettingGraph.DangerSaveRoomPercent.GetValue())
                {
                    FabricGameObject.InstantiatePrefabPoint(PrefsGraph.Instant.elementsData.Chest, interestPlace);
                    if (PrefsGraph.Instant.SettingGraph.EnemyPercent.GetValue())
                    {
                        var spawner = PrefsGraph.Instant.elementsData.MonsterSpawners.First();
                        var elements = subElements.Where(c => c.connectElements.Count > 3 && !(c as Point).pleced).Select(c => c);
                        var pos = PrefsGraph.Instant.SettingGraph.SelectRandElements(countSpawners, elements);
                        foreach (var p in pos)
                        {
                            FabricGameObject.InstantiatePrefab(spawner, p.transform.position, p.transform);
                        }

                    }
                    //if (PrefsGraph.Instant.SettingGraph.TrapPercent.GetValue())
                    //{

                    //}
                }
                else
                {
                    FabricGameObject.InstantiatePrefabPoint(PrefsGraph.Instant.elementsData.Bonfire, interestPlace);
                }
        }
    }

}
