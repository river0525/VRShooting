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
        if (other.gameObject.tag == "Player") //Ž‹ŠE‚Ì”ÍˆÍ“à‚Ì“–‚½‚è”»’è
        {
            //Ž‹ŠE‚ÌŠp“x“à‚ÉŽû‚Ü‚Á‚Ä‚¢‚é‚©
            Vector3 posDelta = other.transform.position - transform.position;
            float target_angle = Vector3.Angle(transform.forward, posDelta);

            if (target_angle < angle) //target_angle‚ªangle‚ÉŽû‚Ü‚Á‚Ä‚¢‚é‚©‚Ç‚¤‚©
            {
                if (Physics.Raycast(transform.position, posDelta, out RaycastHit hit)) //Ray‚ðŽg—p‚µ‚Ätarget‚É“–‚½‚Á‚Ä‚¢‚é‚©”»•Ê
                {
                    if (hit.collider == other) inThisArea = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") inThisArea = false;
    }
}
