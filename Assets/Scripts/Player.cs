using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("à⁄ìÆÇÃê›íË")]
    [SerializeField] float walkSpeed = 1;
    [SerializeField] float runSpeed = 2;
    [SerializeField] float runSP = 1f;
    [SerializeField] float recoverSP = 2f;
    [SerializeField] float jumpSpeed = 1;
    [SerializeField] float maxJumpHeight = 10;
    [SerializeField] float gravity = 1;
    [SerializeField] float headRayDistance = 1;
    [SerializeField] float headRayRadius = 1;
    [SerializeField] Transform headRayOrigin;
    [SerializeField] AudioClip[] footstepsSE;

    [Header("èeÇÃê›íË")]
    [SerializeField] float bulletInterval;
    [SerializeField] float maxRayDistance = 100;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;
    [SerializeField] AudioClip shotSE;

    private float startJumpHeight;
    private float bulletIntervalCount = 0;
    private float footstepsIntervalCount = 0;
    private bool isJump = false;
    private bool isHit = false;
    private CharacterController characterController;
    private RaycastHit hit;
    private Vector3 moveVec;

    private static Transform playerTransform;
    public static bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform = transform;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        PlayerMove();
        Shot();
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

    private void PlayerMove()
    {
        //É^ÉCÉ}Å[ëùâ¡
        footstepsIntervalCount += Time.deltaTime;

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
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        moveVec = (horizontalRotation * new Vector3(x, 0, z)).normalized;
        if (Input.GetButton("Run") && GManager.instance.PlayerSP > 0 && moveVec != Vector3.zero)
        {
            moveVec *= runSpeed;
            GManager.instance.PlayerSP -= runSP * Time.deltaTime;
            PlayFootstepsSE(0.3f);
        }
        else
        {
            moveVec *= walkSpeed;
            GManager.instance.PlayerSP += recoverSP * Time.deltaTime;
            PlayFootstepsSE(0.5f);
        }
        var jumpVec = Vector3.up * y;

        characterController.Move((moveVec + jumpVec) * Time.deltaTime);
    }

    private void Shot()
    {
        //íeÇÃèàóù
        bulletIntervalCount += Time.deltaTime;
        if (Input.GetButton("Shot") && bulletIntervalCount >= bulletInterval)
        {
            int centerX = Screen.width / 2;
            int centerY = Screen.height / 2;
            Vector3 origin = Camera.main.ScreenToWorldPoint(new Vector3(centerX, centerY, 1.5f));//1.5fÇÕèeå˚ÇÃí∑Ç≥
            if (Physics.Raycast(origin, Camera.main.transform.rotation * Vector3.forward, out RaycastHit hit, maxRayDistance)) muzzle.transform.LookAt(hit.point);
            else muzzle.transform.LookAt(Camera.main.transform.rotation * (Camera.main.transform.position + Vector3.forward * maxRayDistance));
            GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
            Bullet bulletScript = b.GetComponent<Bullet>();
            bulletScript.maxMoveDistance = maxRayDistance;
            GManager.instance.PlaySE(shotSE);
            bulletIntervalCount = 0;
        }
    }

    private void PlayFootstepsSE(float interval)
    {
        if (characterController.isGrounded && footstepsIntervalCount >= interval && moveVec != Vector3.zero)
        {
            int randomIndex = Random.Range(0, footstepsSE.Length - 1);
            GManager.instance.PlaySE(footstepsSE[randomIndex]);
            footstepsIntervalCount = 0f;
        }
    }

    public static Vector3 GetPosition()
    {
        return playerTransform.position;
    }
}
