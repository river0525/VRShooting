using UnityEngine;

[System.Serializable]
public class BaseItemGetter : MonoBehaviour
{
    const int getItemSE = 9;
    public void Searched(int itemID, GameObject destroyObj)
    {
        AudioManager.instance.PlaySE(getItemSE);
        PlayerManager.instance.SetItem(itemID);
        Destroy(destroyObj);
    }
}
