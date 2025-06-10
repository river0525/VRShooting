using UnityEngine;

public class UIRotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Camera.main.transform.position - transform.position;
        dir.y = 0;
        if (dir.sqrMagnitude < 0.001f) return;

        Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = rot;
    }
}
