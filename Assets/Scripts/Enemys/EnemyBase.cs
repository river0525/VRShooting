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
    [SerializeField] private float stoppingDistance = 3f;
    [SerializeField] private TrackingArea trackingArea;
    [SerializeField] private Animator animator;

    private HP hp;
    private float attackTimer = 0f;
    private float LockOffDistance = 20f;
    private string animMoveFlag = "isMove";
    private string animAttackFlag = "isAttack";
    private string animDeadFlag = "isDead";
    private bool damaged = false;
    private bool isRandomMove = false;
    private bool isRandomMoveWait = false;
    private bool canMove = true;
    private NavMeshAgent navMeshAgent;
    private Vector3 centralPos;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        var nowHP = maxHP;
        hp = new HP(nowHP, maxHP);
        centralPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
    }
    public void MoveToPlayer()
    {
        if (!canMove) return;
        if (!IsLockOn()) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animAttackFlag)) return;
        ResetRandomMoveFlags();
        animator.speed = 1;
        var playerPos = PlayerMover.GetPosition();
        var dis = Vector3.Distance(playerPos, transform.position);
        if (dis <= stoppingDistance) return;
        navMeshAgent.stoppingDistance = stoppingDistance;
        navMeshAgent.speed = runSpeed;
        navMeshAgent.SetDestination(playerPos);
        animator.SetBool(animMoveFlag, true);
    }
    public void Attack()
    {
        if (!canMove) return;
        if (!IsLockOn()) return;
        var playerPos = PlayerMover.GetPosition();
        var dis = Vector3.Distance(playerPos, transform.position);
        if (dis > stoppingDistance) return;
        if (attackTimer < attackInterval) return;
        attackTimer = 0f;
        animator.SetTrigger(animAttackFlag);
    }
    public void LookAtPlayer()
    {
        if (!canMove) return;
        var playerPos = PlayerMover.GetPosition();
        var dis = Vector3.Distance(playerPos, transform.position);
        if (dis > stoppingDistance) return;
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
        navMeshAgent.destination = randomPos;
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
        navMeshAgent.destination = transform.position;
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
        var playerPos = PlayerMover.GetPosition();
        var dis = Vector3.Distance(playerPos, transform.position);
        if (dis >= LockOffDistance)
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
        navMeshAgent.destination = transform.position;
        animator.speed = 0;
    }
    public void Restart()
    {
        if (canMove) return;
        canMove = true;
        animator.speed = 1;
    }
}
