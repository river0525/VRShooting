using UnityEngine;

public class EnemyChallenge : MonoBehaviour, ISearchableObj
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private GameObject[] destroyObj;
    [SerializeField] private GameObject reward;
    [SerializeField] private float time = 1.5f;
    [SerializeField] private string gotRewardFlag;

    private bool isSearched =false;
    private bool isCleared = false;

    const int appearSE = 0;
    const int clearSE = 14;
    const string challengeFlag = "敵討伐挑戦中";
    private const string shotTutorial = "Shot";
    private const string shotTutorialExplataion = "右トリガー：撃つ";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetChallenge();
        reward.SetActive(false);
        if (FlagDataBase.Instance.GetFlag(gotRewardFlag))
        {
            isCleared = true;
            DestroyObjs();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSearched || isCleared) return;
        if (Timer.IsTime())
        {
            ResetChallenge();
            return;
        }
        isCleared = TryClear();
    }
    public void Searched()
    {
        if (FlagDataBase.Instance.GetFlag(challengeFlag)) return;
        if (isSearched || isCleared) return;
        Timer.SetCounter(time);
        TutorialManager.instance.DoTutorial(shotTutorial, shotTutorialExplataion);
        FlagDataBase.Instance.SetFlag(challengeFlag, true);
        isSearched = true;
        AudioManager.instance.PlaySE(appearSE);
        foreach (var enemy in enemys) enemy.SetActive(true);
    }

    private void ResetChallenge()
    {
        isSearched = false;
        FlagDataBase.Instance.SetFlag(challengeFlag, false);
        foreach (var enemy in enemys) enemy.SetActive(false);
    }

    private bool TryClear()
    {
        if (isCleared) return true;
        foreach (var enemy in enemys) if (enemy.activeInHierarchy) return false;
        AudioManager.instance.PlaySE(clearSE);
        Timer.ResetCounter();
        reward.SetActive(true);
        FlagDataBase.Instance.SetFlag(challengeFlag, false);
        DestroyObjs();
        return true;
    }

    private void DestroyObjs()
    {
        foreach (var obj in destroyObj) Destroy(obj);
    }
}
