using UnityEngine;

[CreateAssetMenu(fileName = "EventItem", menuName = "Scriptable Objects/Item/EventItem")]
public class EventItem : ItemData,IRemoveItem
{
    [SerializeField] string gotItemFlag;
    public override bool Use()
    {
        return false;
    }
    public void OnRemove()
    {
        FlagDataBase.Instance.SetFlag(gotItemFlag, false);
    }
}
