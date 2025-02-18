using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMover : MonoBehaviour
{
    [Header("移動の設定")]
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
    [SerializeField] LineRenderer laserPointer;

    [Header("銃の設定")]
    [SerializeField] float bulletInterval;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;
    [SerializeField] AudioClip shotSE;
    [SerializeField] GameObject gun;

    private float startJumpHeight;
    private float bulletIntervalCount = 0;
    private float footstepsIntervalCount = 0;
    private bool isJump = false;
    private bool isHit = false;
    private CharacterController characterController;
    private RaycastHit hit;
    private Vector3 moveVec;
    private PlayerStatus playerStatus;

    private static Transform playerTransform;
    public static bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStatus = GetComponent<PlayerStatus>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform = transform;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            laserPointer.enabled = true;
            gun.SetActive(false);
            return;
        }
        laserPointer.enabled = false;
        gun.SetActive(true);
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
        //タイマー増加
        footstepsIntervalCount += Time.deltaTime;

        //移動処理
        var y = -gravity;
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

        if (characterController.isGrounded && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
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
        var vrCamera = GameObject.Find("PlayerSet/OVRCameraRig/TrackingSpace/CenterEyeAnchor").transform;
        var horizontalRotation = Quaternion.AngleAxis(vrCamera.eulerAngles.y, Vector3.up);
        moveVec = (horizontalRotation * new Vector3(input.x, 0, input.y)).normalized;
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch) && playerStatus.GetSP() > 0 && moveVec != Vector3.zero)
        {
            moveVec *= runSpeed;
            playerStatus.SubtractSP(runSP * Time.deltaTime);
            PlayFootstepsSE(0.3f);
        }
        else
        {
            moveVec *= walkSpeed;
            playerStatus.AddSP(recoverSP * Time.deltaTime);
            PlayFootstepsSE(0.5f);
        }
        var jumpVec = Vector3.up * y;

        characterController.Move((moveVec + jumpVec) * Time.deltaTime);
    }

    private void Shot()
    {
        //弾の処理
        bulletIntervalCount += Time.deltaTime;
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && bulletIntervalCount >= bulletInterval)
        {
            int centerX = Screen.width / 2;
            int centerY = Screen.height / 2;
            Instantiate(bullet, muzzle.position, muzzle.rotation);
            AudioManager.instance.PlaySE(shotSE);
            bulletIntervalCount = 0;
        }
    }

    private void PlayFootstepsSE(float interval)
    {
        if (characterController.isGrounded && footstepsIntervalCount >= interval && moveVec != Vector3.zero)
        {
            int randomIndex = Random.Range(0, footstepsSE.Length - 1);
            AudioManager.instance.PlaySE(footstepsSE[randomIndex]);
            footstepsIntervalCount = 0f;
        }
    }

    public static Vector3 GetPosition()
    {
        return playerTransform.position;
    }
}
