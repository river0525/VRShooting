using UnityEngine;

public class FireWall : MonoBehaviour
{
    [SerializeField] AudioClip evaporationSE;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(FlagManager.CheckFlag(FlagManager.FlagName.digested)) Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (!OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) return;
        if (!FlagManager.CheckFlag(FlagManager.FlagName.getBucket))
        {
            PlayerStatus.SetPurpose("バケツをさがせ！");
            return;
        }
        FlagManager.EnableFlag(FlagManager.FlagName.digested);
        AudioManager.instance.PlaySE(evaporationSE);
        PlayerStatus.SetPurpose("先に進もう！");
        Destroy(gameObject);
    }
}
