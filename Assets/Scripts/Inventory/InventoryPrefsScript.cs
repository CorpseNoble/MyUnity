using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [ExecuteAlways]
    public class InventoryPrefsScript : MonoBehaviour
    {
        public InventoryPrefs inventoryPrefs = InventoryPrefs.Instant;
    }

    [Serializable,ExecuteAlways]
    public class InventoryPrefs
    {
        public ItemsData itemsData;
        public TraitData traitData;
        public GradeData gradeData;

        #region singletone 
        public static InventoryPrefs Instant
        {
            get
            {
                if (instant == null)
                    instant = new InventoryPrefs();
                return instant;
            }
        }

        private static InventoryPrefs instant;

        private InventoryPrefs() { }
        #endregion
    }
}
