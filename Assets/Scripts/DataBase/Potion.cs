using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Item/Potion")]
public class Potion : ItemData,IUsableItem
{
    [SerializeField] int recoverHP;

    const int healingSE = 22;
    public bool TryUse()
    {
        PlayerManager.instance.AddHP(recoverHP);
        AudioManager.instance.PlaySE(healingSE);
        return true;
    }
}
