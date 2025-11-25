using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public enum PlayerState
{
    Ready,
    Attacking,
    Dead
}

public class PlayerStatus : MonoBehaviour, IAttackable, IDamagable
{
    public PlayerState state;

    [Header("플레이어 최초 스탯")]
    public int playerLevel = 1;
    public int playerMaxHp = 100;
    public int playerCurrentHp = 100;
    public int playerMaxMp = 100;
    public int playerCurrentMp = 0;
    public int playerAtk = 10;
    public int playerDef = 10;
    public int playerMaxExp = 30;
    public int playerExp = 0;
    [Range(5f, 20f)] public float playerSpeed = 5.0f;

    private IDamagable Idamageable;

    public Transform target;

    private void Start()
    {
        state = PlayerState.Ready;
    }

    private void Update()
    {
        if (state == PlayerState.Dead)
        {
            GameManager.Instance.PlayerInfo.PlayerDead();
        }
        else
        {

        }
    }

    public void Movement()
    {

    }

    public void Attack(int atkDamage)
    {
        Idamageable.Damaged(atkDamage);
        MpCharge();
    }

    public void Damaged(int damage)
    {
        playerCurrentHp = Mathf.Max(0, playerCurrentHp - damage);

        if (playerCurrentHp == 0)
        {
            state = PlayerState.Dead;
        }
    }

    private void MpCharge()
    {
        playerCurrentMp += 20;
    }

    public void AddExp(int exp)
    {
        playerExp += exp;

        if (playerExp >= playerMaxExp)
        {
            playerExp -= playerMaxExp;
            LevelUp();
        }
    }

    private void ExpPerLevel()
    {
        playerMaxExp *= 2;
    }

    private void LevelUp()
    {
        playerMaxHp += 100;
        playerAtk += 1;
        playerDef += 1;
        ExpPerLevel();
    }
}
