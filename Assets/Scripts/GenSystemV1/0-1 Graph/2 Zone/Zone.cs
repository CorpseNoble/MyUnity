using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public abstract class Zone : GraphElement
    {
        [Header("Zone")]
        public int waySize = 5;
        public int roomSize = 10;

        public override void Generate()
        {
            waySize = PrefsGraph.Instant.SettingGraph.waySize.GetValue();
            roomSize = PrefsGraph.Instant.SettingGraph.roomSize.GetValue();
        }

        /// <summary>
        /// Select and setup room on currentPos
        /// </summary>
        /// <param name="currentVector"></param>
        /// <param name="currentPos"></param>
        /// <returns></returns>
        protected GraphElement SelectionRoom(Vector3 currentVector, Vector3 currentPos, bool endEl = false)
        {
            GraphElement currElem;
            if (PrefsGraph.Instant.SettingGraph.roomPercent.GetValue() || endEl)
                if (PrefsGraph.Instant.SettingGraph.roudRoomPercent.GetValue())
                    currElem = FabricGameObject.InstantiateElement<Round>(currentPos, this, currentVector);
                else
                    currElem = FabricGameObject.InstantiateElement<Squar>(currentPos, this, currentVector);
            else
                currElem = FabricGameObject.InstantiateElement<Way>(currentPos, this, currentVector);

            return currElem;
        }

    }

}
