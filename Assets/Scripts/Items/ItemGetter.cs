using Unity.VisualScripting;
using UnityEngine;

public class ItemGetter : MonoBehaviour
{
    [SerializeField] int itemID;

    bool gotten = false;

    const int getItemSE = 9;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (!OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) return;
        if (gotten) return;
        gotten = true;
        AudioManager.instance.PlaySE(getItemSE);
        PlayerManager.instance.SetItem(itemID);
        Destroy(gameObject);
    }
}
