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
        public List<GameItem> gameItems = new List<GameItem>();
    }

    [Serializable]
    public class GameItem
    {
        public GameItemType itemType = (GameItemType)0;
        public GameObject gameObject;
        public string Name;
        public string Discription;
        public List<Stat> Effects;
    }
    [Serializable]
    public class Stat
    {
        public MainStatType mainStatType;
        public int Value;
    }

    [Serializable]
    public class StatEffect: Stat
    {
        public bool Percent = false;
        public bool UnDuration = false;
        public int Duration;
    }
    public enum GameItemType
    {
        WeaponOneH,
        WeaponTwoH,
        Shild,
        ArmorUpper,
        ArmorLower,
        ArmorArm,
        ArmorLeg,
        ArmorBoot,
        ArmorHead,
        Ring,
        Collar,
        Poution,
        Scroll,
    }

    public enum MainStatType
    {
        HP,           //очки здоровья
        MP,           //очки маны
        SP,           //спец. очки
        Stress,       //стресс
        Pain,         //боль
        Fatigue,      //усталость

        Strength,     //сила:         +++PhAttack ++Width               +HP +SP +Pain
        Agility,      //ловкость:     ++Acc +CrRate ++Avade             +HP +SP +Pain
        Vitality,     //выносливость: +++PhRes +MagRes +Width           +++HP ++SP ++Pain ++Fatigue
        Endurance,    //стойкость:    +PhRes +++MagRes +Width           ++HP +++SP ++Pain ++Fatigue   
        Intelligence, //интеллект:    +++MagAttack                      +MP +++Stress
        Knowledge,    //знания:       +Acc ++CrRate                     +++MP ++Stress 
        
        Luck,         //удача:        ++DropRate +Acc +CrRate
    }

    public enum SecStatType
    {
        PhAttack,
        MagAttack,
        PhRes,
        MagRes,
        Accuracity,
        CritRate,
        Width,
        Avade,
        DropRate,
    }
}
