using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Item/Potion")]
public class Potion : ItemData
{
    [SerializeField] int recoverHP;

    const int healingSE = 22;
    public override bool Use()
    {
        PlayerManager.instance.AddHP(recoverHP);
        AudioManager.instance.PlaySE(healingSE);
        return true;
    }
}
