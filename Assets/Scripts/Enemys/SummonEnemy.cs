using UnityEngine;

public class SummonEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;

    private bool isSummoned = false;
    private EnemyBase enemyBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        foreach(var enemy in enemies) enemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((float)enemyBase.hp.Get() / enemyBase.hp.GetMax() > 0.1f) return;
        if ((float)enemyBase.hp.Get() == 0)
        {
            if (!isSummoned) return;
            isSummoned = false;
            foreach (var enemy in enemies) if(enemy != null) enemy.SetActive(false);
            return;
        }
        if (isSummoned) return;
        isSummoned = true;
        foreach (var enemy in enemies) enemy.SetActive(true);
    }
}
