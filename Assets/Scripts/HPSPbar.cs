using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSPbar : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] Image spBar;
    public static bool stopBar = false;
    // Start is called before the first frame update
    void Start()
    {
        stopBar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopBar)
        {
            hpBar.fillAmount = GManager.instance.PlayerHP / (float)GManager.instance.maxPlayerHP;
            spBar.fillAmount = GManager.instance.PlayerSP / GManager.instance.maxPlayerSP;
        }
    }
}
