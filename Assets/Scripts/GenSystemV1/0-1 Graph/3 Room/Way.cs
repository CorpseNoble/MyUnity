﻿using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public class Way : Room
    {
        public override void Generate()
        {
            Vector3 currentPos = transform.position;
            var pathLenght = PrefsGraph.Instant.SettingGraph.pathLenght.GetValue();

            for (int j = 0; j < pathLenght; j++)
            {

                var se = FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector);

                if (j > 0)
                {
                    subElements[j - 1].Connect(se);
                }
                subElements.Add(se);

                if (blacklist.Contains(currentPos) || PrefsGraph.Instant.SettingGraph.noizePercent.GetValue())
                    if (PrefsGraph.Instant.SettingGraph.LRPercent.GetValue())
                        buildVector.ToTheLeft();
                    else
                        buildVector.ToTheRight();

                if (PrefsGraph.Instant.SettingGraph.noizePercent.GetValue())
                {
                    if (PrefsGraph.Instant.SettingGraph.LRPercent.GetValue())
                    {
                        if (!blacklist.Contains(se.transform.position + buildVector.ToLeft()*scale))
                            newWays.Add((se, se.transform.position + buildVector.ToLeft() * scale, buildVector.ToLeft()));
                    }
                    else
                    {
                        if (!blacklist.Contains(se.transform.position + buildVector.ToRight() * scale))
                            newWays.Add((se, se.transform.position + buildVector.ToRight() * scale, buildVector.ToRight()));
                    }
                }
                currentPos += buildVector * scale;

                if (blacklist.Contains(currentPos))
                    break;

            }
            newWays.Insert(0, (subElements.Last(), subElements.Last().transform.position + buildVector * scale, buildVector));
            newWays.Add((subElements.Last(), subElements.Last().transform.position + buildVector.ToLeft() * scale, buildVector.ToLeft()));
            newWays.Add((subElements.Last(), subElements.Last().transform.position + buildVector.ToRight() * scale, buildVector.ToRight()));
            rootElement = subElements.First();
            if (backElement != null)
            {
                backElement.Connect(rootElement);
            }
            foreach (var e in subElements)
            {

                e.Generate();
            }


        }
    }

}
