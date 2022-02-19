using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public class Way : Room
    {
        public override void Generate()
        {
            Vector3 currentPos = transform.position;
            var waySize = (parentElement as Zone).waySize;

            for (int j = 0; j < waySize; j++)
            {

                var se = FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector);

                if (j > 0)
                {
                    subElements[j - 1].Connect(se);
                }
                subElements.Add(se);



                if (PrefsGraph.Instant.SettingGraph.noizePercent.GetValue())
                {
                    if (PrefsGraph.Instant.SettingGraph.LRPercent.GetValue())
                    {
                        if (!blacklist.Contains(se.transform.position.StepH(buildVector.ToLeft())))
                            newWays.Add(new NewWay(se, se.transform.position.StepH(buildVector.ToLeft()), buildVector.ToLeft()));
                    }
                    else
                    {
                        if (!blacklist.Contains(se.transform.position.StepH(buildVector.ToRight())))
                            newWays.Add(new NewWay(se, se.transform.position.StepH(buildVector.ToRight()), buildVector.ToRight()));
                    }
                }
                currentPos = currentPos.StepH(buildVector);

                if (blacklist.Contains(currentPos) || PrefsGraph.Instant.SettingGraph.noizePercent.GetValue())
                    if (PrefsGraph.Instant.SettingGraph.LRPercent.GetValue())
                        buildVector.ToTheLeft();
                    else
                        buildVector.ToTheRight();

                if (blacklist.Contains(currentPos))
                    break;

            }
            var lastPoint = subElements.Last();
            var lastPos = lastPoint.transform.position;
            newWays.Add(new NewWay(lastPoint, lastPos.StepH(buildVector), buildVector));
            newWays.Add(new NewWay(lastPoint, lastPos.StepH(buildVector.ToLeft()), buildVector.ToLeft()));
            newWays.Add(new NewWay(lastPoint, lastPos.StepH(buildVector.ToRight()), buildVector.ToRight()));
            rootElement = subElements.First();

            GenPointEntry(true);


        }
    }

}
