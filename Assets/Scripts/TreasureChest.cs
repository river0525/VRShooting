using UnityEngine;

public class TreasureChest : MonoBehaviour, ISearchableObj
{
    [SerializeField] GameObject gameClearText;
    [SerializeField] GameObject exclamationMark;
    [SerializeField] GameObject BGM;

    const int clearSE = 21;
    const string getTreasureFlag = "•ó‚ðŽè‚É“ü‚ê‚½";

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        gameClearText.SetActive(false);
        exclamationMark.SetActive(true);
        BGM.SetActive(true);
    }
    public void Searched()
    {
        if (FlagDataBase.Instance.GetFlag(getTreasureFlag)) return;
        FlagDataBase.Instance.SetFlag(getTreasureFlag, true);
        AudioManager.instance.PlaySE(clearSE);
        anim.SetBool("isOpened", true);
        gameClearText.SetActive(true);
        exclamationMark.SetActive(false);
        BGM.SetActive(false);
    }
}
