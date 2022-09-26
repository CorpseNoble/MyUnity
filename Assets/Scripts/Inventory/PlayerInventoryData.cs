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
                playerTrait.Trait.level.CurExp += 1;
            }
            else
            {
                playerTrait.Trait.level.CurExp += 1;
            }
            UpdateStat();

        }
        public void AddItem(PlayerItem playerItem)
        {
            var itemsInPlayer = playerItems.Where(c => c.GameItem == playerItem.GameItem).First();
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
                if (trait.Trait.level.CurLevel <= 0) continue;

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
                status.UpMainStat(sec.tType, (int)(status.GetStat(sec.tType, status.playerMainStats).Value * (sec.value * 0.01f)));
            }

            status.OnStatusChanged();
        }
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

        [NonSerialized] private Trait _trait;

        [SerializeField] private int _traitCategoryID = 0;
        [SerializeField] private int _traitID = 0;
    }


    [Serializable]
    public class PlayerItem
    {
        public virtual GameItem GameItem { get => _gameItem; }

        [SerializeField] public int quantity = 1;

        [NonSerialized] protected GameItem _gameItem;

        [SerializeField] protected int _gameItemID = 0;
        [SerializeField] protected int _gradeID = 0;

    }

    [Serializable]
    public class PlayerWeapon : PlayerItem
    {
        public WeaponItem WeaponItem => GameItem as WeaponItem;
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
        public EqupmentItem EqupmentItem => GameItem as EqupmentItem;
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
        public ConsumableItem ConsumableItem => GameItem as ConsumableItem;
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
        public OterItem OterItem => GameItem as OterItem;
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
