using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("à⁄ìÆÇÃê›íË")]
    [SerializeField] float walkSpeed = 1;
    [SerializeField] float jumpSpeed = 1;
    [SerializeField] float maxJumpHeight = 10;
    [SerializeField] float gravity = 1;

    [Header("èeÇÃê›íË")]
    [SerializeField] float bulletInterval;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;

    private float startJumpHeight;
    private float bulletIntervalCount = 0;
    private bool isJump = false;
    private CharacterController characterController;
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
        if(isJump)
        {
            if (transform.position.y - startJumpHeight > maxJumpHeight) isJump = false;
            else y = jumpSpeed;
        }
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var moveVec = horizontalRotation * new Vector3(x, 0, z) * walkSpeed;
        var jumpVec = Vector3.up * y;

        characterController.Move((moveVec + jumpVec) * Time.deltaTime);

        //íeÇÃèàóù
        bulletIntervalCount += Time.deltaTime;
        if(Input.GetButton("Shot") && bulletIntervalCount >= bulletInterval)
        {
            Instantiate(bullet, muzzle.position, muzzle.rotation);
            bulletIntervalCount = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack") Debug.Log("GameOver");
    }
}
