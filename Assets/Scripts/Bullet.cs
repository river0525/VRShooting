using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] string[] ignoreTag;
    [SerializeField] float maxMoveDistance = 100;

    private Vector3 startPos;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PlayerMover.canMove) return;
        if (Vector3.Distance(startPos, transform.position) > maxMoveDistance) Destroy(gameObject);
        else rb.MovePosition(transform.position + transform.rotation * Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger) Destroy(gameObject);
    }
}
