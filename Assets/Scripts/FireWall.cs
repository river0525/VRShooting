using UnityEngine;

public class FireWall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        PlayerStatus.SetPurpose("先に進もう！");
        Destroy(gameObject);
    }
}
