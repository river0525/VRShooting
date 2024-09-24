using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSPbar : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] Image spBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = GManager.instance.PlayerHP / (float)GManager.instance.maxPlayerHP;
        spBar.fillAmount = GManager.instance.PlayerSP / GManager.instance.maxPlayerSP;
    }
}
