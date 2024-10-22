using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleAttacker : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int damage = 1;

    private int enemyID;
    private PlayerStatus playerStatus;
    private ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    private void Start()
    {
        enemyID = enemy.GetInstanceID();
        ps = GetComponent<ParticleSystem>();
        var playerObj = GameObject.FindWithTag("Player");
        playerStatus = playerObj.GetComponent<PlayerStatus>();
        ps.trigger.SetCollider(0, playerStatus);
    }
    void OnParticleTrigger()
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        if (numEnter >= 1) playerStatus.PlayerDamage(damage, enemyID);
    }
}
