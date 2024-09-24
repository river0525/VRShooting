using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int damage = 1;

    private int enemyID;
    private void Start()
    {
        enemyID = enemy.GetInstanceID();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GManager.instance.PlayerDamage(damage, enemyID);
        }
    }
}
