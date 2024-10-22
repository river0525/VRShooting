using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour,IEnemyAttack
{
    [SerializeField] private float attackAreaRadius = 3f;

    private string biteAttackFlag = "isAttack";
    private Animator animator;
    private EnemyBase enemyBase;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyBase = GetComponent<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        animator.SetBool(biteAttackFlag, true);
    }

    public bool IsAttack()
    {
        return animator.GetBool(biteAttackFlag);
    }
    public bool InAttackArea()
    {
        return enemyBase.GetPlayerDistance() <= attackAreaRadius;
    }
}
