using Unity.VisualScripting;
using UnityEngine;

public class ItemGetter : SearchableObj
{
    [SerializeField] int itemID;

    const int getItemSE = 9;
    public override void Searched()
    {
        AudioManager.instance.PlaySE(getItemSE);
        PlayerManager.instance.SetItem(itemID);
        Destroy(gameObject);
    }
}
