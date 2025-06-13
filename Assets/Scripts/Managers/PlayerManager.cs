using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    [SerializeField] int maxHP = 10;
    [SerializeField] int maxSP = 10;

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

    public const int itemNullNum = -1;
    const int playerDamageSE = 6;
    private const string useItemTutorial = "UseItem";
    private const string useItemTutorialExplanation = "Bボタン：アイテム";

    private HP hp;
    private float sp;
    private int lastEnemyID;
    private int heldItem;
    private float timer = 0f;
    private HPSPbar hpSPBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;//Startより前に実行されるので、Awakeで登録
            Initialize();
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        lastEnemyID = gameObject.GetInstanceID();//自分のIDで初期化
        var hpSpBarObj = GameObject.FindWithTag("HPSPBar");
        hpSPBar= hpSpBarObj.GetComponent<HPSPbar>();
        hpSPBar.UpdateAll();
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void PlayerDamage(int damage, int enemyID)
    {
        if ((enemyID == lastEnemyID && timer < playerDamageInterval) || !PlayerMover.canMove) return;
        lastEnemyID = enemyID;
        timer = 0f;
        hp.Subtract(damage);
        hpSPBar.UpdateHP();
        AudioManager.instance.PlaySE(playerDamageSE);
        if (GetHP() == 0) GameManager.instance.GameOver();
    }
    public void SetItem(int itemID)
    {
        if (heldItem != itemNullNum) ThrowAwayItem();
        if(ItemDataBase.Instance.CanUse(itemID)) TutorialManager.instance.DoTutorial(useItemTutorial, useItemTutorialExplanation);
        heldItem = itemID;
        hpSPBar.UpdateIcon();
    }

    private void ThrowAwayItem()
    {
        if (heldItem == itemNullNum) return;
        var prefab = ItemDataBase.Instance.GetPrefab(heldItem);
        Instantiate(prefab, PlayerMover.GetPosition(), PlayerMover.GetRotarion());
        ItemDataBase.Instance.OnThrowAway(heldItem);
        heldItem = itemNullNum;
    }
    public int GetItem()
    {
        return heldItem;
    }
    public void UseItem()
    {
        if (heldItem == itemNullNum || !ItemDataBase.Instance.TryUse(heldItem)) return;
        TutorialManager.instance.DoneTutorial(useItemTutorial);
        LostItem();
    }

    public void LostItem()
    {
        heldItem = itemNullNum;
        hpSPBar.UpdateIcon();
    }

    public int GetHP()
    {
        return hp.Get();
    }
    public int GetMaxHP()
    {
        return hp.GetMax();
    }
    public void AddHP(int amount)
    {
        hp.Add(amount);
        hpSPBar.UpdateHP();
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
        hpSPBar.UpdateSP();
    }
    public void SubtractSP(float amount)
    {
        SP -= amount;
        hpSPBar.UpdateSP();
    }

    public void Initialize()
    {
        var nowHP = maxHP;
        hp = new HP(nowHP, maxHP);
        SP = maxSP;
        heldItem = itemNullNum;
    }

    public void Retry()
    {
        hp.FullRecover();
        heldItem = itemNullNum;
    }
}
