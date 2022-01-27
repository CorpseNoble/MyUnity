using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.GenSystemV1
{
    public class Connect
    {
        public readonly GraphElement[] Elements;
        public bool instanted = false;

        public Connect lowConnect = null;
        public Connect(GraphElement elem1, GraphElement elem2)
        {
            Elements = new GraphElement[] { elem1, elem2 };
        }
        public GraphElement GetConnect(GraphElement element)
        {
            if (!Elements.Contains(element))
                throw new Exception("not connected element");

            if (Elements[0] == element)
                return Elements[1];
            else
                return Elements[0];
        }
        public override bool Equals(object obj)
        {
            if (obj is Connect con)
                return (con.Elements[0] == Elements[0] || con.Elements[1] == Elements[0])
                    && (con.Elements[0] == Elements[1] || con.Elements[1] == Elements[1]);
            else
                return false;
        }

        public override int GetHashCode()
        {
            int hashCode = 580546273;
            hashCode = hashCode * -1521134295 + EqualityComparer<GraphElement[]>.Default.GetHashCode(Elements);
            hashCode = hashCode * -1521134295 + EqualityComparer<Connect>.Default.GetHashCode(lowConnect);
            return hashCode;
        }
    }

}
