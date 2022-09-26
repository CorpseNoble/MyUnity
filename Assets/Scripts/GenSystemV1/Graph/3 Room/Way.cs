using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public class Way : Room
    {
        public bool stairs = false;
        public override void Generate()
        {
            Vector3 currentPos = transform.position;
            var waySize = (parentElement as Zone).waySize;

            for (int j = 0; j < waySize; j++)
            {
                if (stairs && j < waySize - 1 && j > 0)
                    currentPos = currentPos.StepV(-1);
                var se = FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector);
                se.stairs = stairs && (j < waySize - 1) && j > 0;
                if (j > 0)
                {
                    subElements[j - 1].Connect(se);
                }
                subElements.Add(se);


                if (!stairs)
                    if (PrefsGraph.Instant.SettingGraph.noizePercent.GetValue())
                    {
                        if (PrefsGraph.Instant.SettingGraph.LRPercent.GetValue())
                        {
                            if (!blacklist.CheckPos(se.transform.position.StepH(buildVector.ToLeft())))
                                newWays.Add(new NewWay(se, se.transform.position.StepH(buildVector.ToLeft()), buildVector.ToLeft()));
                        }
                        else
                        {
                            if (!blacklist.CheckPos(se.transform.position.StepH(buildVector.ToRight())))
                                newWays.Add(new NewWay(se, se.transform.position.StepH(buildVector.ToRight()), buildVector.ToRight()));
                        }
                    }
                currentPos = currentPos.StepH(buildVector);

                if (blacklist.CheckPos(currentPos) || PrefsGraph.Instant.SettingGraph.noizePercent.GetValue())
                    if (PrefsGraph.Instant.SettingGraph.LRPercent.GetValue())
                        buildVector.ToTheLeft();
                    else
                        buildVector.ToTheRight();

                if (blacklist.CheckPos(currentPos))
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
