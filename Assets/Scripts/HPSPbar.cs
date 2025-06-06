using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSPbar : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] Image spBar;
    [SerializeField] Image icon;
    [SerializeField] Sprite nullIcon;

    public void UpdateAll()
    {
        UpdateHP();
        UpdateSP();
        UpdateIcon();
    }
    public void UpdateHP()
    {
        hpBar.fillAmount = PlayerManager.instance.GetHP() / (float)PlayerManager.instance.GetMaxHP();
    }
    public void UpdateSP()
    {
        spBar.fillAmount = PlayerManager.instance.GetSP() / (float)PlayerManager.instance.GetMaxSP();
    }

    public void UpdateIcon()
    {
        var itemID = PlayerManager.instance.GetItem();
        if (itemID == PlayerManager.itemNullNum) icon.sprite = nullIcon;
        else icon.sprite = ItemDataBase.Instance.GetIcon(itemID);
    }
}
