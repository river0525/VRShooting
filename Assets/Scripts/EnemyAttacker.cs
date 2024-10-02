using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int damage = 1;

    private int enemyID;
    private PlayerStatus playerStatus;
    private void Start()
    {
        enemyID = enemy.GetInstanceID();
        var playerObj = GameObject.FindWithTag("Player");
        playerStatus = playerObj.GetComponent<PlayerStatus>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerStatus.PlayerDamage(damage, enemyID);
        }
    }
}
