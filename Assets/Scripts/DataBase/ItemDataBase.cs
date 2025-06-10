using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "Scriptable Objects/ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    private static ItemDataBase instance = null;
    public static ItemDataBase Instance
    {
        get
        {
            if (instance == null) instance = Resources.Load<ItemDataBase>("ItemDataBase");
            return instance;
        }
    }

    [SerializeField, NonReorderable] private ItemData[] itemData;

    public bool Use(int idx)
    {
        return itemData[idx].Use();
    }
    public Sprite GetIcon(int idx)
    {
        return itemData[idx].Icon;
    }
    public GameObject GetPrefab(int idx)
    {
        return itemData[idx].Prefab;
    }
    public void OnThrowAway(int idx)
    {
        if (itemData[idx] is IRemoveItem item) item.OnRemove();
    }
}
