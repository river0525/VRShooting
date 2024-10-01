using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    public int maxPlayerHP = 5;
    public int maxPlayerSP = 5;

    public int PlayerHP {
        get
        {
            return playerHP;
        }
        set
        {
            if (value < 0) playerHP = 0;
            else if (value > maxPlayerHP) playerHP = maxPlayerHP;
            else playerHP = value;
        } 
    }

    public float PlayerSP
    {
        get
        {
            return playerSP;
        }
        set
        {
            if (value < 0 && value < playerSP) playerSP = -5;
            else if (value > maxPlayerSP) playerSP = maxPlayerSP;
            else playerSP = value;
        }
    }
    [SerializeField] float playerDamageInterval = 0.5f;
    [SerializeField] AudioClip playerDamageSE;

    private int playerHP;
    private float playerSP;
    private int lastEnemyID;
    private float timer = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            lastEnemyID = gameObject.GetInstanceID();//é©ï™ÇÃIDÇ≈èâä˙âª
            playerHP = maxPlayerHP;
            playerSP = maxPlayerSP;
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
        if ((enemyID == lastEnemyID && timer < playerDamageInterval) || !Player.canMove) return;
        lastEnemyID = enemyID;
        timer = 0f;
        PlayerHP -= damage;
        AudioManager.instance.PlaySE(playerDamageSE);
    }
}