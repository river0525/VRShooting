using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    public int playerHP = 5;
    public Transform playerTransform;

    [SerializeField] float playerDamageInterval = 0.5f;

    private int lastEnemyID;
    private float timer = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            lastEnemyID = gameObject.GetInstanceID();//é©ï™ÇÃIDÇ≈èâä˙âª
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void PlayerDamage(int damage, int enemyID)
    {
        if (enemyID == lastEnemyID && timer < playerDamageInterval) return;
        lastEnemyID = enemyID;
        timer = 0f;
        playerHP -= damage;
    }
}