using UnityEngine;

public class Wizard : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private float attackAreaRadius = 3f;

    private string masicAttackFlag = "isAttack";
    private Animator animator;
    private EnemyBase enemyBase;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyBase = GetComponent<EnemyBase>();
    }

    public void Attack()
    {
        animator.SetBool(masicAttackFlag, true);
    }

    public bool IsAttack()
    {
        return animator.GetBool(masicAttackFlag);
    }
    public bool InAttackArea()
    {
        return enemyBase.GetPlayerDistance() <= attackAreaRadius;
    }
}
