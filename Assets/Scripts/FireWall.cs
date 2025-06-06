using UnityEngine;

public class FireWall : SearchableObj
{
    const int evaporationSE = 7;
    const string digestedFlag = "‰Î‚ğÁ‚µ‚½";
    const string searchedFlag = "‰Î‚ğ’²‚×‚½";
    const string getBucketFlag = "ƒoƒPƒc‚ğè‚É“ü‚ê‚½";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(FlagDataBase.Instance.GetFlag(digestedFlag)) Destroy(gameObject);
    }
    public override void Searched()
    {
        FlagDataBase.Instance.SetFlag(searchedFlag, true);
        if (!FlagDataBase.Instance.GetFlag(getBucketFlag)) return;
        FlagDataBase.Instance.SetFlag(digestedFlag, true);
        AudioManager.instance.PlaySE(evaporationSE);
        Destroy(gameObject);
    }
}
