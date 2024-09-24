using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("à⁄ìÆÇÃê›íË")]
    [SerializeField] float walkSpeed = 1;
    [SerializeField] float runSpeed = 2;
    [SerializeField] float jumpSpeed = 1;
    [SerializeField] float maxJumpHeight = 10;
    [SerializeField] float gravity = 1;
    [SerializeField] float headRayDistance = 1;
    [SerializeField] float headRayRadius = 1;
    [SerializeField] Transform headRayOrigin;

    [Header("èeÇÃê›íË")]
    [SerializeField] float bulletInterval;
    [SerializeField] float maxRayDistance = 100;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;

    private float startJumpHeight;
    private float bulletIntervalCount = 0;
    private bool isJump = false;
    private bool isHit = false;
    private CharacterController characterController;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //à⁄ìÆèàóù
        var x = Input.GetAxis("Horizontal");
        var y = -gravity;
        var z = Input.GetAxis("Vertical");
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            isJump = true;
            startJumpHeight = transform.position.y;
        }
        isHit = Physics.SphereCast(headRayOrigin.position, headRayRadius, Vector3.up, out hit, headRayDistance);
        if (isJump)
        {
            if (transform.position.y - startJumpHeight > maxJumpHeight || isHit) isJump = false;
            else y = jumpSpeed;
        }
        Debug.Log(isJump);
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var moveVec = (horizontalRotation * new Vector3(x, 0, z)).normalized;
        moveVec *= Input.GetButton("Run") ? runSpeed : walkSpeed;
        var jumpVec = Vector3.up * y;

        characterController.Move((moveVec + jumpVec) * Time.deltaTime);

        //íeÇÃèàóù
        bulletIntervalCount += Time.deltaTime;
        if(Input.GetButton("Shot") && bulletIntervalCount >= bulletInterval)
        {
            int centerX = Screen.width / 2;
            int centerY = Screen.height / 2;
            Vector3 origin = Camera.main.ScreenToWorldPoint(new Vector3(centerX, centerY, 1.5f));//1.5fÇÕèeå˚ÇÃí∑Ç≥
            if (Physics.Raycast(origin, Camera.main.transform.rotation * Vector3.forward, out RaycastHit hit, maxRayDistance)) muzzle.transform.LookAt(hit.point);
            else muzzle.transform.LookAt(Camera.main.transform.rotation * (Camera.main.transform.position + Vector3.forward * maxRayDistance));
            GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
            Bullet bulletScript = b.GetComponent<Bullet>();
            bulletScript.maxMoveDistance = maxRayDistance;
            bulletIntervalCount = 0;
        }
    }

    private void OnDrawGizmos()
    {
        if (isHit)
        {
            Gizmos.DrawRay(headRayOrigin.position, Vector3.up * hit.distance);
            Gizmos.DrawWireSphere(headRayOrigin.position + Vector3.up * (hit.distance), headRayRadius);
        }
        else
        {
            Gizmos.DrawRay(headRayOrigin.position, Vector3.up * 100);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack") Debug.Log(GManager.instance.playerHP);
    }
}
