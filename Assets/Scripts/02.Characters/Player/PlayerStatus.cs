using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    Attacking,
    Hit,
    Dead
}

public class PlayerStatus : MonoBehaviour, IDamagable
{
    public PlayerState state;

    [Header("플레이어 최초 스탯")]
    public int playerLevel = 1;
    public int playerMaxHp = 3000;
    public int playerCurrentHp = 3000;
    public int playerMaxMp = 100;
    public int playerCurrentMp = 0;
    public int playerAtk = 10;
    public int playerDef = 10;
    public float playerMaxExp = 30f;
    public float playerExp = 0;

    public Transform target;

    private void Start()
    {
        state = PlayerState.Idle;
    }

    private void Update()
    {
        if (state == PlayerState.Dead)
        {
            GameManager.Instance.PlayerInfo.PlayerDead();
        }
    }

    public void Equip(int value, EquipmentType type)
    {
        if(type == EquipmentType.Weapon)
        {
            playerAtk += value;
        }
        else if(type == EquipmentType.Aromor)
        {
            playerDef += value;
        }
    }


    public void Damaged(int damage)
    {
        playerCurrentHp = Mathf.Max(0, playerCurrentHp - damage);
        GameManager.Instance.PlayerInfo.OnPlayerAnime(PlayerState.Hit);

        if (playerCurrentHp == 0)
        {
            state = PlayerState.Dead;
            GameManager.Instance.PlayerInfo.OnPlayerAnime(PlayerState.Dead);
        }
    }

    public void MpCharge()
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
        for(int i = 0; i < playerLevel; i++)
        {
            if(playerLevel == 1)
            {
                playerMaxExp = 30;
            }
            else
            {
                if(playerLevel % 5 != 0)
                {
                    playerMaxExp *= 1.5f;
                }
                else
                {
                    playerMaxExp *= 3f;
                }
            }
        }
    }

    private void LevelUp()
    {
        playerLevel += 1;
        playerMaxHp += 100;
        playerAtk += 1;
        playerDef += 1;
        ExpPerLevel();
    }
}
