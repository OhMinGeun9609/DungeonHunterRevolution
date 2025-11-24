using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Potion,
    Etc
}

public enum PotionType
{
    Heal,
    PlusStatus
}

public enum CharacterType
{
    Player,
    Enemy,
    Boss
}

public enum GameStatus
{
    Started,
    Paused,
    Resume,
    StageClear,
    Clear,
    GameOver
}

public class EnumManager : SingletonManager<EnumManager>
{
    public ItemType itemType;
    public PotionType PotionType;
    public CharacterType characterType;
    public GameStatus status;
}
