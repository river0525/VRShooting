using UnityEngine;

public class InstantiateObj : MonoBehaviour
{
    [SerializeField] Transform origin;
    public void InstantiateObjMethod(GameObject obj)
    {
        Instantiate(obj,origin.position,origin.rotation);
    }
}
