using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ItemData data;
    private Enemy enemy;
    private PlayerStatus player;
    private bool isPlayer;
    private int atk;
    private Rigidbody rb;

    private void Start()
     {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        enemy = transform.root.GetComponent<Enemy>();
        player = GetComponentInParent<PlayerStatus>();
        atk = data.atk;

        isPlayer = player != null;

        if(isPlayer)
        {
            Debug.Log("PlayerOn");
        }
    }

    private void Init()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name +
          ", ROOT: " + collision.transform.root.name);

        if (collision.transform.root == transform.root)
            return;

        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable inter))
        {
            if(isPlayer && collision.gameObject.GetComponent<Enemy>())
                inter.Damaged(GetDamage(atk));
            else if (!isPlayer && collision.gameObject.CompareTag("Player"))
                inter.Damaged(GetDamage(atk));
        }
    }

    private int GetDamage(int atk)
    {
        int damage = atk * (Random.Range(5, 11));

        return damage;
    }
}
