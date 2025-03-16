using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] int maxHP = 5;
    [SerializeField] int maxSP = 5;

    private float SP
    {
        get
        {
            return sp;
        }
        set
        {
            if (value < 0 && value < sp) sp = -5;
            else if (value > maxSP) sp = maxSP;
            else sp = value;
        }
    }
    [SerializeField] float playerDamageInterval = 0.5f;
    [SerializeField] AudioClip playerDamageSE;
    [SerializeField] TextMeshProUGUI purposeText;

    private static HP hp;
    private static float sp = -100f;
    private static string purpose = "先に進もう！";
    private int lastEnemyID;
    private float timer = 0f;

    private void Start()
    {
        lastEnemyID = gameObject.GetInstanceID();//自分のIDで初期化
        var nowHP = maxHP;
        if(hp == null) hp = new HP(nowHP, maxHP);
        if(SP == -100f) SP = maxSP;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        purposeText.text = purpose;
    }

    public void PlayerDamage(int damage, int enemyID)
    {
        if ((enemyID == lastEnemyID && timer < playerDamageInterval) || !PlayerMover.canMove) return;
        lastEnemyID = enemyID;
        timer = 0f;
        hp.Subtract(damage);
        AudioManager.instance.PlaySE(playerDamageSE);
    }

    public int GetHP()
    {
        return hp.Get();
    }
    public int GetMaxHP()
    {
        return hp.GetMax();
    }
    public void FullRecover()
    {
        hp.FullRecover();
    }
    public float GetSP()
    {
        return SP;
    }
    public float GetMaxSP()
    {
        return maxSP;
    }
    public void AddSP(float amount)
    {
        SP += amount;
    }
    public void SubtractSP(float amount)
    {
        SP -= amount;
    }

    public static void SetPurpose(string text)
    {
        purpose = text;
    }

    public static void Reset()
    {
        hp = null;
        sp = -100f;
        purpose = "先に進もう！";
    }
}
