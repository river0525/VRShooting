using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckSE : MonoBehaviour,IPointerUpHandler
{
    const int volumeCheckSE = 16;
    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.instance.PlaySE(volumeCheckSE);
    }
}
