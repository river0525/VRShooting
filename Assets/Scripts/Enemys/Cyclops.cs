using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cyclops : MonoBehaviour,IEnemyAttack
{
    [SerializeField] float swingAttackRadius = 5f;
    [SerializeField] float shockWaveAttackRadius = 20f;

    private string swingAttackFlag = "isSwingAttack";
    private string shockWaveAttackFlag = "isShockWaveAttack";
    private EnemyBase enemyBase;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        animator = GetComponentInChildren<Animator>();
        if (swingAttackRadius > shockWaveAttackRadius) Debug.Log("swingAttackDistance < shockWaveAttackDistance‚Æ‚È‚é‚æ‚¤’l‚ðÝ’è‚µ‚Ä‚­‚¾‚³‚¢");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack()
    {
        if(enemyBase.GetPlayerDistance() <= swingAttackRadius)
        {
            animator.SetBool(swingAttackFlag, true);
            return;
        }
        animator.SetBool(shockWaveAttackFlag, true);
    }

    public bool IsAttack()
    {
        return animator.GetBool(swingAttackFlag) || animator.GetBool(shockWaveAttackFlag);
    }
    public bool InAttackArea()
    {
        return enemyBase.GetPlayerDistance() <= shockWaveAttackRadius;
    }
}
