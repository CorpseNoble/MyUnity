using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New Player Inventory Data", menuName = "Player Inventory Data", order = 52)]
    public class PlayerInventoryData : ScriptableObject
    {
        public Status status = new Status();
        public Equpment equipment = new Equpment();

        public List<PlayerTrait> playerTraits = new List<PlayerTrait>();
        public List<PlayerItem> playerItems = new List<PlayerItem>();

        public void AddTrait(PlayerTrait playerTrait)
        {
            var traitInPlayer = playerTraits.Where(c => c.Trait == playerTrait.Trait).First();
            if (traitInPlayer == null)
            {
                playerTraits.Add(playerTrait);
                playerTrait.Experience += Trait.Exp_Per_One;
            }
            else
            {
                traitInPlayer.Experience += Trait.Exp_Per_One;
            }
            UpdateStat();
        }
        public void AddItem(PlayerItem playerItem)
        {
            var itemsInPlayer = playerItems.Where(c => c.GameItem == playerItem.GameItem && c.Grade == playerItem.Grade).First();
            if (itemsInPlayer == null)
            {
                playerItems.Add(playerItem);
            }
            else
            {
                itemsInPlayer.quantity += playerItem.quantity;
            }
        }
        [ContextMenu("UpdateStat")]
        public void UpdateStat()
        {
            List<Stat<MainStatType>> pircentStats = new List<Stat<MainStatType>>();
            status.Clear();
            foreach (var trait in playerTraits)
            {
                if (trait.level <= 0) continue;

                foreach (var eff in trait.Trait.statEffects)
                {
                    if (!eff.percent)
                    {
                        status.UpMainStat(eff.tType, eff.value);
                    }
                    else
                    {
                        pircentStats.Add(eff);
                    }
                }
            }
            foreach (var place in equipment.armorPlace.equpmentPlaces)
            {
                if (place.free) continue;

                var equpItem = place.playerItem.GameItem as EqupmentItem;
                foreach (var eff in equpItem.statEffects)
                {
                    if (!eff.percent)
                    {
                        status.UpMainStat(eff.tType, eff.value);
                    }
                    else
                    {
                        pircentStats.Add(eff);
                    }
                }
            }
            foreach (var place in equipment.weaponPlace.equpmentPlaces)
            {
                if (place.free) continue;

                var weaponItem = place.playerItem.GameItem as WeaponItem;
                foreach (var eff in weaponItem.statEffects)
                {
                    if (!eff.percent)
                    {
                        status.UpMainStat(eff.tType, eff.value);
                    }
                    else
                    {
                        pircentStats.Add(eff);
                    }
                }
            }

            foreach (var sec in pircentStats)
            {
                status.UpMainStat(sec.tType, (int)(status.GetStat(sec.tType, status.playerMainStats).value * (sec.value * 0.01f)));
            }


        }
    }
    [Serializable]
    public class Status
    {
        public PlayerBarStats<BarStatType> HP
        {
            get => playerBarStats.Where(c => c.stat == BarStatType.HP).First();
            //{
            //    if (_HP == null)
            //        _HP = 
            //    return _HP;
            //}
        }
        public PlayerBarStats<BarStatType> MP
        {
            get => playerBarStats.Where(c => c.stat == BarStatType.MP).First();
            //{
            //    if (_MP == null)
            //        _MP 
            //    return _MP;
            //}
        }
        public PlayerBarStats<BarStatType> SP
        {
            get => playerBarStats.Where(c => c.stat == BarStatType.SP).First();
            //{
            //    if (_SP == null)
            //        _SP 
            //    return _SP;
            //}
        }

        public PlayerBarStats<BarStatType> Pain => playerBarStats.Where(c => c.stat == BarStatType.Pain).First();
        public PlayerBarStats<BarStatType> Fatigue => playerBarStats.Where(c => c.stat == BarStatType.Fatigue).First();
        public PlayerBarStats<BarStatType> Stress => playerBarStats.Where(c => c.stat == BarStatType.Stress).First();


        [SerializeField] public List<PlayerStats<MainStatType>> playerMainStats;
        [SerializeField] public List<PlayerStats<SecStatType>> playerSecStats;
        [SerializeField] public List<PlayerBarStats<BarStatType>> playerBarStats;

        private PlayerBarStats<BarStatType> _HP;
        private PlayerBarStats<BarStatType> _MP;
        private PlayerBarStats<BarStatType> _SP;

        [SerializeField, Range(1, 9)] private int _lowEff = 1;
        [SerializeField, Range(1, 9)] private int _medEff = 3;
        [SerializeField, Range(1, 9)] private int _higEff = 5;
        public Status()
        {
            FillStat(out playerMainStats);
            FillStat(out playerSecStats);
            FillBarStat(out playerBarStats);
        }
        public void Clear()
        {
            playerBarStats.Clear();
            playerMainStats.Clear();
            playerSecStats.Clear();

            FillStat(out playerMainStats);
            FillStat(out playerSecStats);
            FillBarStat(out playerBarStats);
        }
        public void FillStat<T>(out List<PlayerStats<T>> playerStats) where T : struct
        {
            playerStats = new List<PlayerStats<T>>();
            var stats = typeof(T).GetEnumValues();

            foreach (T stat in stats)
            {
                var playerstat = new PlayerStats<T>() { stat = stat };
                playerStats.Add(playerstat);
            }
        }
        public void FillBarStat<T>(out List<PlayerBarStats<T>> playerStats) where T : struct
        {
            playerStats = new List<PlayerBarStats<T>>();
            var stats = typeof(T).GetEnumValues();

            foreach (T stat in stats)
            {
                var playerstat = new PlayerBarStats<T>() { stat = stat };
                playerStats.Add(playerstat);
            }
        }
        public void UpMainStat(MainStatType mainStat, int value)
        {
            GetStat(mainStat, playerMainStats).value += value;

            switch (mainStat)
            {
                case MainStatType.Strength:

                    GetStat(SecStatType.PhAttack, playerSecStats).value += value * _higEff;
                    GetStat(SecStatType.Weight, playerSecStats).value += value * _medEff;

                    GetBarStat(BarStatType.HP, playerBarStats).valueMax += value * _lowEff;
                    GetBarStat(BarStatType.SP, playerBarStats).valueMax += value * _lowEff;
                    GetBarStat(BarStatType.Pain, playerBarStats).valueMax += value * _lowEff;

                    break;
                case MainStatType.Agility:

                    GetStat(SecStatType.Accuracity, playerSecStats).value += value * _medEff;
                    GetStat(SecStatType.CritRate, playerSecStats).value += value * _lowEff;
                    GetStat(SecStatType.Avade, playerSecStats).value += value * _medEff;

                    GetBarStat(BarStatType.HP, playerBarStats).valueMax += value * _lowEff;
                    GetBarStat(BarStatType.SP, playerBarStats).valueMax += value * _lowEff;
                    GetBarStat(BarStatType.Pain, playerBarStats).valueMax += value * _lowEff;

                    break;
                case MainStatType.Vitality:

                    GetStat(SecStatType.PhRes, playerSecStats).value += value * _higEff;
                    GetStat(SecStatType.MagRes, playerSecStats).value += value * _lowEff;
                    GetStat(SecStatType.Weight, playerSecStats).value += value * _lowEff;

                    GetBarStat(BarStatType.HP, playerBarStats).valueMax += value * _higEff;
                    GetBarStat(BarStatType.SP, playerBarStats).valueMax += value * _medEff;
                    GetBarStat(BarStatType.Pain, playerBarStats).valueMax += value * _medEff;
                    GetBarStat(BarStatType.Fatigue, playerBarStats).valueMax += value * _medEff;

                    break;
                case MainStatType.Endurance:

                    GetStat(SecStatType.PhRes, playerSecStats).value += value * _lowEff;
                    GetStat(SecStatType.MagRes, playerSecStats).value += value * _higEff;
                    GetStat(SecStatType.Weight, playerSecStats).value += value * _lowEff;

                    GetBarStat(BarStatType.HP, playerBarStats).valueMax += value * _medEff;
                    GetBarStat(BarStatType.SP, playerBarStats).valueMax += value * _higEff;
                    GetBarStat(BarStatType.Pain, playerBarStats).valueMax += value * _medEff;
                    GetBarStat(BarStatType.Fatigue, playerBarStats).valueMax += value * _medEff;

                    break;
                case MainStatType.Intelligence:

                    GetStat(SecStatType.MagAttack, playerSecStats).value += value * _higEff;

                    GetBarStat(BarStatType.MP, playerBarStats).valueMax += value * _lowEff;
                    GetBarStat(BarStatType.Stress, playerBarStats).valueMax += value * _higEff;

                    break;
                case MainStatType.Knowledge:

                    GetStat(SecStatType.Accuracity, playerSecStats).value += value * _lowEff;
                    GetStat(SecStatType.CritRate, playerSecStats).value += value * _medEff;

                    GetBarStat(BarStatType.MP, playerBarStats).valueMax += value * _higEff;
                    GetBarStat(BarStatType.Stress, playerBarStats).valueMax += value * _lowEff;

                    break;
                case MainStatType.Luck:

                    GetStat(SecStatType.DropRate, playerSecStats).value += value * _medEff;
                    GetStat(SecStatType.Accuracity, playerSecStats).value += value * _lowEff;
                    GetStat(SecStatType.CritRate, playerSecStats).value += value * _lowEff;

                    break;

            }
        }
        public PlayerStats<T> GetStat<T>(T stat, List<PlayerStats<T>> stats) where T : struct
        {
            return stats.Where(c => c.stat.Equals(stat)).First();
        }
        public PlayerBarStats<T> GetBarStat<T>(T stat, List<PlayerBarStats<T>> stats) where T : struct
        {
            return stats.Where(c => c.stat.Equals(stat)).First();
        }


    }

    [Serializable]
    public class PlayerStats<T> where T : struct
    {
        public T stat;
        public int value;
    }

    [Serializable]
    public class PlayerBarStats<T> where T : struct
    {
        public T stat;
        public int valueMax;
        public int valueCurrent;
    }


    [Serializable]
    public class PlayerTrait
    {
        public Trait Trait
        {
            get
            {
                if (_trait == null)
                    _trait = InventoryPrefs.Instant.traitData.categoryData[_traitCategoryID].traits[_traitID];
                return _trait;
            }
        }

        public Grade Grade
        {
            get
            {
                if (_grade == null)
                    _grade = InventoryPrefs.Instant.gradeData.Grades[_gradeID];
                return _grade;
            }
        }

        public int Experience { get => _experience; set { if (value >= Trait.expirience) level++; _experience = value; } }

        [SerializeField] public int level = 0;
        [SerializeField] private int _experience = 0;

        [NonSerialized] private Trait _trait;
        [NonSerialized] private Grade _grade;

        [SerializeField] private int _traitCategoryID = 0;
        [SerializeField] private int _traitID = 0;
        [SerializeField] private int _gradeID = 0;

    }

    [Serializable]
    public class Equpment
    {
        public WeaponPlace weaponPlace = new WeaponPlace();
        public ArmorPlace armorPlace = new ArmorPlace();
    }
    [Serializable]
    public class WeaponPlace
    {
        [NonSerialized] public List<EqupmentPlace<WeaponItem.Type, PlayerWeapon>> equpmentPlaces;

        [SerializeField]
        private EqupmentPlace<WeaponItem.Type, PlayerWeapon> _weaponTwoH
            = new EqupmentPlace<WeaponItem.Type, PlayerWeapon>(WeaponItem.Type.WeaponTwoH);

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
                case WeaponItem.Type.WeaponTwoH:

                    if (!_weaponOneH.free) _weaponOneH.UnEqup();
                    if (!_shild.free) _shild.UnEqup();

                    if (!_weaponTwoH.free) _weaponTwoH.UnEqup();
                    _weaponTwoH.Equp(playerWeapon);
                    break;

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
            EqupmentItem equpmentItem = playerEqupment.GameItem as EqupmentItem;
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

    [Serializable]
    public class PlayerItem
    {
        public virtual GameItem GameItem { get => _gameItem; }

        public Grade Grade
        {
            get
            {
                if (_grade == null)
                    _grade = InventoryPrefs.Instant.gradeData.Grades[_gradeID];
                return _grade;
            }
        }

        [SerializeField] public int quantity = 1;

        [NonSerialized] protected GameItem _gameItem;
        [NonSerialized] protected Grade _grade;

        [SerializeField] protected int _gameItemID = 0;
        [SerializeField] protected int _gradeID = 0;

    }
    [Serializable]
    public class PlayerWeapon : PlayerItem
    {
        public override GameItem GameItem
        {
            get
            {
                if (_gameItem == null)
                    _gameItem = InventoryPrefs.Instant.itemsData.weapons[_gameItemID];
                return _gameItem;
            }
        }
    }

    [Serializable]
    public class PlayerEqupment : PlayerItem
    {
        public override GameItem GameItem
        {
            get
            {
                if (_gameItem == null)
                    _gameItem = InventoryPrefs.Instant.itemsData.equpments[_gameItemID];
                return _gameItem;
            }
        }
    }
    [Serializable]
    public class PlayerConsumble : PlayerItem
    {
        public override GameItem GameItem
        {
            get
            {
                if (_gameItem == null)
                    _gameItem = InventoryPrefs.Instant.itemsData.consumbles[_gameItemID];
                return _gameItem;
            }
        }
    }
    [Serializable]
    public class PlayerOther : PlayerItem
    {
        public override GameItem GameItem
        {
            get
            {
                if (_gameItem == null)
                    _gameItem = InventoryPrefs.Instant.itemsData.oters[_gameItemID];
                return _gameItem;
            }
        }
    }

}
