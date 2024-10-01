using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHP = 3;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private float randomMoveInterval = 5f;
    [SerializeField] private float randomMoveBreakTime = 1f;
    [SerializeField] private TrackingArea trackingArea;
    [SerializeField] private Animator animator;

    private HP hp;
    private float attackTimer = 0f;
    private float randomMoveTimer = 0f;
    private float LockOffDistance = 20f;
    private bool isLockOn = false;
    private NavMeshAgent navMeshAgent;

    public static bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        var nowHP = maxHP;
        hp = new HP(nowHP, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            navMeshAgent.destination = transform.position;
            animator.speed = 0;
            return;
        }
        animator.speed = 1;
        attackTimer += Time.deltaTime;
        randomMoveTimer += Time.deltaTime;
        var playerPos = Player.GetPosition();
        var dis = Vector3.Distance(playerPos, transform.position);
        if (dis >= LockOffDistance) isLockOn = false;
        if (trackingArea.inThisArea) isLockOn = true;
        if(isLockOn)
        {
            navMeshAgent.SetDestination(playerPos);
            if (dis < navMeshAgent.stoppingDistance)
            {
                if (attackTimer >= attackInterval)
                {
                    attackTimer = 0f;
                    animator.SetTrigger("isAttack");
                }
                else
                {
                    var direction = playerPos - transform.position;
                    direction.y = 0;

                    var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                    var newRotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
                    if(newRotation==transform.rotation) animator.SetBool("isMove", false);
                    else
                    {
                        animator.SetBool("isMove", true);
                        transform.rotation = newRotation;
                    }
                }
            }
            else animator.SetBool("isMove", true);
        }
        else
        {
            if(randomMoveTimer >= randomMoveInterval)
            {
                if (randomMoveTimer >= randomMoveInterval + randomMoveBreakTime)
                {
                    randomMoveTimer = 0f;
                    navMeshAgent.destination = transform.position;
                }
                
            }
            navMeshAgent.destination = transform.position;
        }
        
    }

    public void CustomizedOnTriggerEnter(Collider other)
    {
        if (!canMove) return;
        if (other.gameObject.tag == "PlayerAttack")
        {
            var damage = 1;
            hp.Subtract(damage);
            isLockOn = true;
            if (hp.Get() == 0) animator.SetBool("isDead", true);
        }
    }
}
