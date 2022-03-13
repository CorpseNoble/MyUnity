using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New Items Data", menuName = "Items Data", order = 52)]
    public class ItemsData : ScriptableObject
    {
        public List<WeaponItem> weapons = new List<WeaponItem>();
        public List<EqupmentItem> equpments = new List<EqupmentItem>();
        public List<ConsumableItem> consumbles = new List<ConsumableItem>();
        public List<OterItem> oters = new List<OterItem>();
    }
    public abstract class NameDisciption
    {
        public string name;
        public string discription;
    }
    [Serializable]
    public abstract class GameItem : NameDisciption
    {
        public Sprite sprite;
        public GameObject gameObject;
        public int price = 100;
    }
    [Serializable]
    public class WeaponItem : GameItem
    {
        public Type type = 0;
        public List<Stat<MainStatType>> statEffects = new List<Stat<MainStatType>>();

        public enum Type
        {
            WeaponOneH,
            Shild,
        }
    }
    [Serializable]
    public class EqupmentItem : GameItem
    {
        public Type type = 0;
        public List<Stat<MainStatType>> statEffects = new List<Stat<MainStatType>>();

        public enum Type
        {
            ArmorUpper,
            ArmorLower,
            ArmorArm,
            ArmorLeg,
            ArmorBoot,
            ArmorHead,
            Ring,
            Collar,
        }
    }
    [Serializable]
    public class ConsumableItem : GameItem
    {
        public Type type = 0;
        public List<Stat<MainStatType>> statEffects = new List<Stat<MainStatType>>();
        public List<StatDuretion<BarStatType>> barEffects = new List<StatDuretion<BarStatType>>();
        public enum Type
        {
            Poution,
            Scroll,
            Food,
        }
    }
    [Serializable]
    public class OterItem : GameItem
    {
        public Type type = 0;
        public enum Type
        {
            Key,
            Material,
            Sellable,
        }
    }
    [Serializable]
    public class Stat<T> where T : struct
    {
        public T tType;
        public int value = 1;
        public bool percent = false;

        public override string ToString()
        {
            return tType.ToString() + " " + value;
        }
    }

    [Serializable]
    public class StatDuretion<T> : Stat<T> where T : struct
    {
        public bool unDuration = false;
        public int duration = 30;
    }
    public enum MainStatType
    {
        Strength,     //сила:         +++PhAttack ++Weight              +HP +SP +Pain
        Agility,      //ловкость:     ++Acc +CrRate ++Avade             +HP +SP +Pain
        Vitality,     //выносливость: +++PhRes +MagRes +Weight          +++HP ++SP ++Pain ++Fatigue
        Endurance,    //стойкость:    +PhRes +++MagRes +Weight          ++HP +++SP ++Pain ++Fatigue   
        Intelligence, //интеллект:    +++MagAttack                      +MP +++Stress
        Knowledge,    //знания:       +Acc ++CrRate                     +++MP ++Stress 

        Luck,         //удача:        ++DropRate +Acc +CrRate
    }

    public enum BarStatType
    {
        HP,           //очки здоровья
        FP,           //очки силы
        MP,           //очки маны
        Stress,       //стресс
        Fatigue,      //усталость
    }
    public enum SecStatType
    {
        PhAttack,
        MagAttack,
        PhRes,
        MagRes,
        Accuracity,
        CritRate,
        Weight,
        Avade,
        DropRate,
    }
}
