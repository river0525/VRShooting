using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAttack
{
    public void Attack();
    public bool IsAttack();
    public bool InAttackArea();
}
