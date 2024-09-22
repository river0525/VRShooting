using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TrackingArea trackingArea;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trackingArea.inThisArea) navMeshAgent.SetDestination(player.position);
        else navMeshAgent.destination = transform.position;
    }

    public void CustomizedOnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerAttack") Destroy(gameObject);
    }
}
