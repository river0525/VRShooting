using UnityEngine;

public class BaseGimmick : MonoBehaviour,ISearchableObj
{
    [SerializeField] private string searchedFlag;
    [SerializeField] private string bootedFlag;
    [SerializeField] private int eventItemID;

    private IGimmickBooter gimmick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (FlagDataBase.Instance.GetFlag(bootedFlag) &&TryGetComponent<IRemovableGimmickOnStart>(out var comp)) comp.RemoveOnStart();
        gimmick =GetComponent<IGimmickBooter>();
    }
    public void Searched()
    {
        FlagDataBase.Instance.SetFlag(searchedFlag, true);
        if (PlayerManager.instance.GetItem() != eventItemID || FlagDataBase.Instance.GetFlag(bootedFlag)) return;
        if (!gimmick.TryBoot()) return;
        PlayerManager.instance.LostItem();
        FlagDataBase.Instance.SetFlag(bootedFlag, true);
    }
}
