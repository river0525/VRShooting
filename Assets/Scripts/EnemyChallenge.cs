using UnityEngine;

public class EnemyChallenge : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private GameObject[] destroyObj;
    [SerializeField] private GameObject reward;
    [SerializeField] private float time = 1.5f;

    bool isSearched=false;
    bool isCleared = false;

    const int appearSE = 0;
    const int clearSE = 14;
    const string challengeFlag = "ìGì¢î∞íßêÌíÜ";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var enemy in enemys) enemy.SetActive(false);
        reward.SetActive(false);
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
        isCleared = true;
        foreach (var enemy in enemys)
        {
            if (!enemy.activeInHierarchy) continue;
            isCleared=false;
            break;
        }
        if(!isCleared) return;
        AudioManager.instance.PlaySE(clearSE);
        Timer.ResetCounter();
        reward.SetActive(true);
        FlagDataBase.Instance.SetFlag(challengeFlag, false);
        foreach (var obj in destroyObj) Destroy(obj);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (!OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) return;
        if (FlagDataBase.Instance.GetFlag(challengeFlag)) return;
        if (isSearched) return;
        Timer.SetCounter(time);
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
}
