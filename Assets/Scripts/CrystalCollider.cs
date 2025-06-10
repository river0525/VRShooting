using UnityEngine;

public class CrystalCollider : MonoBehaviour
{
    [SerializeField] Crystal crystal;
    private void OnTriggerEnter(Collider other)
    {
        crystal.CustomizedOnTriggerEnter(other);
    }
}
