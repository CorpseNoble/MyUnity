using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public abstract class Zone : GraphElement
    {
        [Header("Zone")]
        public int waySize = 5;
        public int roomSize = 10;
        public int zoneSize = 10;

        public override void Generate()
        {
            waySize = PrefsGraph.Instant.SettingGraph.waySize.GetValue();
            roomSize = PrefsGraph.Instant.SettingGraph.roomSize.GetValue();
            zoneSize = PrefsGraph.Instant.SettingGraph.subzoneSize.GetValue();
        }
        [SerializeField] private bool _preRoom = false;
        protected GraphElement SelectionRoom(Vector3 currentVector, Vector3 currentPos, bool endEl = false)
        {
            GraphElement currElem;
            bool room = PrefsGraph.Instant.SettingGraph.roomPercent.GetValue();
            if ((!_preRoom & room) || endEl)
            {
                if (PrefsGraph.Instant.SettingGraph.roundRoomPercent.GetValue())
                    currElem = FabricGameObject.InstantiateElement<Round>(currentPos, this, currentVector);
                else
                    currElem = FabricGameObject.InstantiateElement<Squar>(currentPos, this, currentVector);

                _preRoom = true;
            }
            else
            {
                currElem = FabricGameObject.InstantiateElement<Way>(currentPos, this, currentVector);
                _preRoom = false;
            }

            return currElem;
        }

    }

}
