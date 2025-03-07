using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingArea : MonoBehaviour
{
    [SerializeField] float angle = 45f;

    [HideInInspector] public bool inThisArea = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        //視界の角度内に収まっているか
        Vector3 playerCenter = Vector3.up;//プレイヤーの足元から中心までの高さ補正
        Vector3 posDelta = (other.transform.position + playerCenter) - transform.position;
        float target_angle = Vector3.Angle(transform.forward, posDelta);
        if (target_angle > angle) //target_angleがangleに収まっているかどうか
        {
            inThisArea = false;
            return;
        }
        if (!Physics.Raycast(transform.position, posDelta, out RaycastHit hit)) return;
        if (hit.collider == other) inThisArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") inThisArea = false;
    }
}
