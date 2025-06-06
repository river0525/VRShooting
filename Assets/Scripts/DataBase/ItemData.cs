using UnityEngine;
public abstract class ItemData : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    public abstract bool Use();
}
