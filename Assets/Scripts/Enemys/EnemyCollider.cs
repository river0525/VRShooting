using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] EnemyBase enemy;
    private void OnTriggerEnter(Collider other)
    {
        enemy.CustomizedOnTriggerEnter(other);
    }
}
