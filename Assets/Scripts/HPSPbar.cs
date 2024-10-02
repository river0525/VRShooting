using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSPbar : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] Image spBar;
    public static bool stopBar = false;

    private PlayerStatus playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        stopBar = false;
        var playerObj = GameObject.FindWithTag("Player");
        playerStatus = playerObj.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopBar)
        {
            hpBar.fillAmount = playerStatus.GetHP() / (float)playerStatus.GetMaxHP();
            spBar.fillAmount = playerStatus.GetSP() / playerStatus.GetMaxSP();
        }
    }
}
