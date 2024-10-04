using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    private EnemyBase enemyBase;
    // Start is called before the first frame update
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        NormalAttack();
    }
    public void Move()
    {
        
    }
    public void NormalAttack()
    {

    }
}
