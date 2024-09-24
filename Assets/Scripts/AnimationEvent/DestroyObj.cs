using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    [SerializeField] GameObject destroyObj;
    public void DestroyObjMethod()
    {
        Destroy(destroyObj);
    }
}
