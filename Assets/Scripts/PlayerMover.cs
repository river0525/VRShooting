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
    [SerializeField] float gravity = 1;
    [SerializeField] LineRenderer laserPointer;

    const int footstepsSE = 12;

    [Header("銃の設定")]
    [SerializeField] float bulletInterval;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject gun;

    private const int shotSE = 17;
    private const string walkTutorial = "Walk";
    private const string runTutorial = "Run";
    private const string shotTutorial = "Shot";
    private const string searchTutorial = "Search";
    private const string walkTutorialExplanation = "右スティック：歩く";
    private const string runTutorialExplanation = "右スティック押し込み：走る";
    private const string searchTutorialExplanation = "Aボタン：調べる";

    private float bulletIntervalCount = 0;
    private float footstepsIntervalCount = 0;
    private bool isSearching = false;
    private CharacterController characterController;
    private Vector3 moveVec;

    private static Transform playerTransform;
    public static bool canMove = true;
    private void Awake()
    {
        playerTransform = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;
        TutorialManager.instance.DoTutorial(walkTutorial, walkTutorialExplanation);
        TutorialManager.instance.DoTutorial(runTutorial, runTutorialExplanation);
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
        CheckUseItem();
        isSearching = OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch);
    }
    
    private void CheckUseItem()
    {
        if (!OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch)) return;
        PlayerManager.instance.UseItem();
    }

    private void PlayerMove()
    {
        //タイマー増加
        footstepsIntervalCount += Time.deltaTime;

        //移動処理
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        var vrCamera = GameObject.Find("PlayerSet/OVRCameraRig/TrackingSpace/CenterEyeAnchor").transform;
        var horizontalRotation = Quaternion.AngleAxis(vrCamera.eulerAngles.y, Vector3.up);
        moveVec = (horizontalRotation * new Vector3(input.x, 0, input.y)).normalized;
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.LTouch) && PlayerManager.instance.GetSP() > 0 && moveVec != Vector3.zero)
        {
            moveVec *= runSpeed;
            PlayerManager.instance.SubtractSP(runSP * Time.deltaTime);
            PlayFootstepsSE(0.3f);
            TutorialManager.instance.DoneTutorial(runTutorial);
        }
        else
        {
            moveVec *= walkSpeed;
            PlayerManager.instance.AddSP(recoverSP * Time.deltaTime);
            PlayFootstepsSE(0.5f);
        }
        var y = Vector3.up * -gravity;
        characterController.Move((moveVec + y) * Time.deltaTime);
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
            TutorialManager.instance.DoneTutorial(shotTutorial);
        }
    }

    private void PlayFootstepsSE(float interval)
    {
        if (characterController.isGrounded && footstepsIntervalCount >= interval && moveVec != Vector3.zero)
        {
            AudioManager.instance.PlaySE(footstepsSE);
            TutorialManager.instance.DoneTutorial(walkTutorial);
            footstepsIntervalCount = 0f;
        }
    }

    public static Vector3 GetPosition()
    {
        return playerTransform.position;
    }
    public static Quaternion GetRotarion()
    {
        return playerTransform.rotation;
    }
    
    public IEnumerator SetPosition(Vector3 pos)
    {
        canMove = false;
        playerTransform.position = pos;
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent<ISearchableObj>(out var comp)) return;
        TutorialManager.instance.DoTutorial(searchTutorial, searchTutorialExplanation);
        if (!isSearching) return;
        TutorialManager.instance.DoneTutorial(searchTutorial);
        comp.Searched();
        isSearching = false;
    }
}
