using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 3;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private float randomMoveInterval = 5f;
    [SerializeField] private float randomMoveBreakTime = 1f;
    [SerializeField] private TrackingArea trackingArea;
    [SerializeField] private Animator animator;

    private float attackTimer = 0f;
    private float randomMoveTimer = 0f;
    private float LockOffDistance = 20f;
    private bool isLockOn = false;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (other.gameObject.tag == "PlayerAttack")
        {
            --HP;
            isLockOn = true;
            if (HP <= 0) animator.SetBool("isDead", true);
        }
    }
}
