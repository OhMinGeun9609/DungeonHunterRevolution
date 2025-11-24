using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public interface IAttackable
{
    void Attack(int AtkDamage);
}

public interface IDamagable
{
    void Damaged(int damage);
}
