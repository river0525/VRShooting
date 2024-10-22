using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private int maxHP = 3;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private float MaxRandomMoveDistance = 20f;
    [SerializeField] private float randomMoveTime = 5f;
    [SerializeField] private float randomMoveWaitTime = 1f;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float LockOffDistance = 20f;
    [SerializeField] private TrackingArea trackingArea;
    
    private HP hp;
    private float attackTimer = 0f;
    private string animMoveFlag = "isMove";
    private string animDeadFlag = "isDead";
    private bool damaged = false;
    private bool isRandomMove = false;
    private bool isRandomMoveWait = false;
    private bool canMove = true;
    private NavMeshAgent navMeshAgent;
    private Vector3 centralPos;
    private IEnemyAttack iEnemyAttack;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        iEnemyAttack = GetComponent<IEnemyAttack>();
        animator = GetComponentInChildren<Animator>();
        var nowHP = maxHP;
        hp = new HP(nowHP, maxHP);
        centralPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        MoveToPlayer();
        StartRandomMove();
        JudgeReached();
        Attack();
        LookAtPlayer();
    }
    public void MoveToPlayer()
    {
        if (!canMove) return;
        if (!IsLockOn()) return;
        if (iEnemyAttack.IsAttack()) return;
        ResetRandomMoveFlags();
        animator.speed = 1;
        if (iEnemyAttack.InAttackArea()) return;
        var playerPos = PlayerMover.GetPosition();
        navMeshAgent.speed = runSpeed;
        navMeshAgent.SetDestination(playerPos);
        animator.SetBool(animMoveFlag, true);
    }
    public void Attack()
    {
        if (!canMove) return;
        if (!IsLockOn()) return;
        if (!iEnemyAttack.InAttackArea()) return;
        if (attackTimer < attackInterval) return;
        navMeshAgent.speed = 0;
        attackTimer = 0f;
        iEnemyAttack.Attack();
    }
    public void LookAtPlayer()
    {
        if (!canMove) return;
        if (!IsLockOn()) return;
        if (!iEnemyAttack.InAttackArea()) return;
        navMeshAgent.speed = 0;
        var playerPos = PlayerMover.GetPosition();
        var direction = playerPos - transform.position;
        direction.y = 0;
        var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        var newRotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
        if (newRotation == transform.rotation) animator.SetBool(animMoveFlag, false);
        else
        {
            animator.SetBool(animMoveFlag, true);
            transform.rotation = newRotation;
        }
    }
    private void SetRandomPosition()
    {
        if (IsLockOn()) return;
        var MIN = -MaxRandomMoveDistance;
        var MAX = MaxRandomMoveDistance;
        var xPos = centralPos.x + Random.Range(MIN, MAX);
        var zPos = centralPos.z + Random.Range(MIN, MAX);
        var randomPos = new Vector3(xPos, 0, zPos);
        navMeshAgent.SetDestination(randomPos);
    }
    public void StartRandomMove()
    {
        if (!canMove) return;
        if (IsLockOn()) return;
        if (isRandomMove) return;
        if (isRandomMoveWait) return;
        isRandomMove = true;
        SetRandomPosition();
        navMeshAgent.stoppingDistance = 0f;
        navMeshAgent.speed = walkSpeed;
        animator.SetBool(animMoveFlag, true);
        Invoke("RandomMoveWait", randomMoveTime);
    }
    private void RandomMoveWait()
    {
        if (!isRandomMove) return;
        if (isRandomMoveWait) return;
        CancelInvoke("RandomMoveWait");
        isRandomMove = false;
        isRandomMoveWait = true;
        animator.SetBool(animMoveFlag, false);
        navMeshAgent.speed = 0;
        Invoke("ResetRandomMoveFlags", randomMoveWaitTime);
    }
    public void JudgeReached()
    {
        if (navMeshAgent.destination == transform.position && isRandomMove) RandomMoveWait();
    }
    private void ResetRandomMoveFlags()
    {
        CancelInvoke("ResetRandomMoveFlags");
        isRandomMove = false;
        isRandomMoveWait = false;
    }
    private bool IsLockOn()
    {
        if (GetPlayerDistance() >= LockOffDistance)
        {
            damaged = false;
            return false;
        }
        if (damaged) return true;
        if (!trackingArea.inThisArea) return false;
        return true;
    }
    public void CustomizedOnTriggerEnter(Collider other)
    {
        if (!canMove) return;
        if (other.gameObject.tag == "PlayerAttack")
        {
            var damage = 1;
            hp.Subtract(damage);
            damaged = true;
            if (hp.Get() == 0) animator.SetBool(animDeadFlag, true);
        }
    }
    public void Pause()
    {
        if (!canMove) return;
        canMove = false;
        navMeshAgent.speed = 0;
        animator.speed = 0;
    }
    public void Restart()
    {
        if (canMove) return;
        canMove = true;
        animator.speed = 1;
    }

    public float GetPlayerDistance()
    {
        var playerPos = PlayerMover.GetPosition();
        return Vector3.Distance(playerPos, transform.position);
    }
}
