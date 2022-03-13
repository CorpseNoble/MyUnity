using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [Serializable]
    public class Equpment
    {
        public WeaponPlace weaponPlace = new WeaponPlace();
        public ArmorPlace armorPlace = new ArmorPlace();

        public float currentWeight = 0;
    }

    [Serializable]
    public class WeaponPlace
    {
        [NonSerialized] public List<EqupmentPlace<WeaponItem.Type, PlayerWeapon>> equpmentPlaces;

        [SerializeField]
        private EqupmentPlace<WeaponItem.Type, PlayerWeapon> _weaponOneH
            = new EqupmentPlace<WeaponItem.Type, PlayerWeapon>(WeaponItem.Type.WeaponOneH);
        [SerializeField]
        private EqupmentPlace<WeaponItem.Type, PlayerWeapon> _shild
             = new EqupmentPlace<WeaponItem.Type, PlayerWeapon>(WeaponItem.Type.Shild);

        public WeaponPlace()
        {
            equpmentPlaces = new List<EqupmentPlace<WeaponItem.Type, PlayerWeapon>>();

            var fildsInfo = typeof(Equpment).GetFields();
            foreach (var fo in fildsInfo)
            {
                if (fo.FieldType == typeof(EqupmentPlace<WeaponItem.Type, PlayerWeapon>))
                {
                    equpmentPlaces.Add(fo.GetValue(this) as EqupmentPlace<WeaponItem.Type, PlayerWeapon>);
                }
            }
        }

        public void EqupWeapon(PlayerWeapon playerWeapon)
        {
            WeaponItem weaponItem = playerWeapon.GameItem as WeaponItem;

            switch (weaponItem.type)
            {
                case WeaponItem.Type.WeaponOneH:

                    if (!_weaponOneH.free) _weaponOneH.UnEqup();
                    _weaponOneH.Equp(playerWeapon);
                    break;

                case WeaponItem.Type.Shild:

                    if (!_shild.free) _shild.UnEqup();
                    _shild.Equp(playerWeapon);
                    break;
            }
        }
    }
    [Serializable]
    public class ArmorPlace
    {
        [NonSerialized] public List<EqupmentPlace<EqupmentItem.Type, PlayerEqupment>> equpmentPlaces;

        [SerializeField]
        private EqupmentPlace<EqupmentItem.Type, PlayerEqupment> _armorHead
            = new EqupmentPlace<EqupmentItem.Type, PlayerEqupment>(EqupmentItem.Type.ArmorHead);
        [SerializeField]
        private EqupmentPlace<EqupmentItem.Type, PlayerEqupment> _armorUpper
            = new EqupmentPlace<EqupmentItem.Type, PlayerEqupment>(EqupmentItem.Type.ArmorUpper);
        [SerializeField]
        private EqupmentPlace<EqupmentItem.Type, PlayerEqupment> _armorLower
            = new EqupmentPlace<EqupmentItem.Type, PlayerEqupment>(EqupmentItem.Type.ArmorLower);
        [SerializeField]
        private EqupmentPlace<EqupmentItem.Type, PlayerEqupment> _armorArm
            = new EqupmentPlace<EqupmentItem.Type, PlayerEqupment>(EqupmentItem.Type.ArmorArm);
        [SerializeField]
        private EqupmentPlace<EqupmentItem.Type, PlayerEqupment> _armorLeg
            = new EqupmentPlace<EqupmentItem.Type, PlayerEqupment>(EqupmentItem.Type.ArmorLeg);
        [SerializeField]
        private EqupmentPlace<EqupmentItem.Type, PlayerEqupment> _armorBoot
            = new EqupmentPlace<EqupmentItem.Type, PlayerEqupment>(EqupmentItem.Type.ArmorBoot);
        [SerializeField]
        private EqupmentPlace<EqupmentItem.Type, PlayerEqupment> _ring
            = new EqupmentPlace<EqupmentItem.Type, PlayerEqupment>(EqupmentItem.Type.Ring);
        [SerializeField]
        private EqupmentPlace<EqupmentItem.Type, PlayerEqupment> _ring2
          = new EqupmentPlace<EqupmentItem.Type, PlayerEqupment>(EqupmentItem.Type.Ring);
        [SerializeField]
        private EqupmentPlace<EqupmentItem.Type, PlayerEqupment> _collar
            = new EqupmentPlace<EqupmentItem.Type, PlayerEqupment>(EqupmentItem.Type.Collar);

        public ArmorPlace()
        {
            equpmentPlaces = new List<EqupmentPlace<EqupmentItem.Type, PlayerEqupment>>();

            var fildsInfo = typeof(Equpment).GetFields();
            foreach (var fo in fildsInfo)
            {
                if (fo.FieldType == typeof(EqupmentPlace<EqupmentItem.Type, PlayerEqupment>))
                {
                    equpmentPlaces.Add(fo.GetValue(this) as EqupmentPlace<EqupmentItem.Type, PlayerEqupment>);
                }
            }
        }

        public void EqupArmor(PlayerEqupment playerEqupment)
        {
            EqupmentItem equpmentItem = playerEqupment.EqupmentItem;
            var equpPlace = equpmentPlaces.Where(w => w.type == equpmentItem.type).First();
            if (!equpPlace.free)
                equpPlace.UnEqup();
            equpPlace.Equp(playerEqupment);
        }
    }
    [Serializable]
    public class EqupmentPlace<T, K> where T : struct
                                     where K : PlayerItem
    {
        public T type;
        public bool free = true;
        public K playerItem;

        public EqupmentPlace(T type)
        {
            this.type = type;
        }
        public void Equp(K item)
        {
            playerItem = item;
            free = false;
        }
        public void UnEqup()
        {
            playerItem = null;
            free = true;
        }
    }

}
