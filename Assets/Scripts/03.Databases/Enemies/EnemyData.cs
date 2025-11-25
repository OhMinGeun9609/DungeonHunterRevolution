using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    General,
    Special
}

public enum AttackType
{
    LongRange,
    ShortRange
}
[System.Serializable]
public class EnemyAttackRange
{
    public AttackType attackType;
    public float value;
}

[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    public string enemyName;
    public EnemyType enemyType;
    public int enemyMaxHp;
    public int enemyCurrentHp;
    public int atk;
    public int def;
    public int rewardExp;
    public GameObject enemyPrefab;

    [Header("AttackRange")]
    public EnemyAttackRange enemyAttackRange;
}
