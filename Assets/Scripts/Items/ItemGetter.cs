using Unity.VisualScripting;
using UnityEngine;

public class ItemGetter : MonoBehaviour, ISearchableObj
{
    [SerializeField] int itemID;

    BaseItemGetter baseItemGetter = new();

    public void Searched()
    {
        baseItemGetter.Searched(itemID, gameObject);
    }
}
