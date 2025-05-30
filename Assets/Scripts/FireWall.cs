using UnityEngine;

public class FireWall : MonoBehaviour
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (!OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) return;
        FlagDataBase.Instance.SetFlag(searchedFlag, true);
        if (!FlagDataBase.Instance.GetFlag(getBucketFlag)) return;
        FlagDataBase.Instance.SetFlag(digestedFlag, true);
        AudioManager.instance.PlaySE(evaporationSE);
        Destroy(gameObject);
    }
}
