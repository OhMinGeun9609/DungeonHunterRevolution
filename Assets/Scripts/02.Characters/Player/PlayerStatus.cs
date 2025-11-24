using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IAttackable, IDamagable
{
    [Header("플레이어 스탯")]
    public int playerMaxHp = 100;
    public int playerCurrentHp = 100;
    public int playerMaxMp = 100;
    public int playerCurrentMp = 0;
    public int playerAtk = 10;
    public int playerDef = 10;
    [Range(5f, 20f)] public float playerSpeed = 5.0f;

    private IAttackable Iattackable;
    private IDamagable Idamageable;

    public Transform target;

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    public void Attack(int atkDamage)
    {
        Idamageable.Damaged(atkDamage);
        MpCharge();
    }

    public void Damaged(int damage)
    {
        playerCurrentHp -= damage;
    }

    private void MpCharge()
    {
        playerCurrentMp += 20;
    }
}
