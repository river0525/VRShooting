using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject dropItem;

    private EnemyBase enemyBase;
    private bool dropped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        dropItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBase.hp.Get() != 0) return;
        if (dropped) return;
        dropped = true;
        PlayerStatus.SetPurpose("•ó” ‚ðŠJ‚¯‚Ä‚Ý‚æ‚¤");
        dropItem.transform.position = new Vector3(transform.position.x, dropItem.transform.position.y, transform.position.z);
        dropItem.SetActive(true);
    }
}
