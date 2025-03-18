using UnityEngine;

public class CrystalCollider : MonoBehaviour
{
    [SerializeField] Crystal crystal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        crystal.CustomizedOnTriggerEnter(other);
    }
}
