using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops : MonoBehaviour
{
    private EnemyBase enemyBase;
    // Start is called before the first frame update
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyBase.MoveToPlayer();
        enemyBase.StartRandomMove();
        enemyBase.JudgeReached();
        enemyBase.Attack();
        enemyBase.LookAtPlayer();
    }
}
