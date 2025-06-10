using UnityEngine;

public class EventItemGetter : MonoBehaviour, ISearchableObj
{
    [SerializeField] int itemID;
    [SerializeField] string alreadyUsedFlag;
    [SerializeField] string getFlag;

    BaseItemGetter baseItemGetter = new();

    public void Start()
    {
        if (FlagDataBase.Instance.GetFlag(alreadyUsedFlag)) Destroy(gameObject);
    }

    public void Searched()
    {
        FlagDataBase.Instance.SetFlag(getFlag, true);
        baseItemGetter.Searched(itemID, gameObject);
    }
}
