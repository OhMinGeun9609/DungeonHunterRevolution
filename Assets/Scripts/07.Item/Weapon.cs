using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ItemData data;
    private Enemy enemy;
    private Player player;
    private bool isPlayer;
    private int atk;

    private void Start()
    {
        enemy = transform.root.GetComponent<Enemy>();
        player = transform.root.GetComponent<Player>();
        atk = data.atk;

        if(enemy == null && player == null)
        {
            isPlayer = false;
        }
        else if(enemy != null)
        {
            isPlayer = false;
        }
        else if(player != null)
        {
            isPlayer = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isPlayer)
        {
            if(collision.gameObject.TryGetComponent<IDamagable>(out IDamagable component))
            {
                component.Damaged(GetDamage(atk));
            }
        }
    }

    private int GetDamage(int atk)
    {
        int damage = atk * (Random.Range(5, 11));

        return damage;
    }
}
