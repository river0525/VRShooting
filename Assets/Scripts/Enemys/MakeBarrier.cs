using UnityEngine;

public class MakeBarrier : MonoBehaviour
{
    [SerializeField] GameObject barrier;
    [SerializeField] GameObject crystal;

    private EnemyBase enemyBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        barrier.SetActive(false);
        crystal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((float)enemyBase.hp.Get() / enemyBase.hp.GetMax() > 0.1f) return;
        barrier.SetActive(true);
        crystal.SetActive(true);
    }
}
