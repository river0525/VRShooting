using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckSE : MonoBehaviour,IPointerUpHandler
{
    [SerializeField] AudioClip checkSE;
    public void OnPointerUp(PointerEventData eventData)
    {
        GManager.instance.PlaySE(checkSE);
    }
}
