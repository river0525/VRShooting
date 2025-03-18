using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] GameObject gameClearText;
    [SerializeField] GameObject exclamationMark;
    [SerializeField] GameObject BGM;
    [SerializeField] AudioClip clearSE;

    private bool isOpened = false;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        gameClearText.SetActive(false);
        exclamationMark.SetActive(true);
        BGM.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (!OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) return;
        if (isOpened) return;
        isOpened = true;
        AudioManager.instance.PlaySE(clearSE);
        anim.SetBool("isOpened", true);
        gameClearText.SetActive(true);
        exclamationMark.SetActive(false);
        BGM.SetActive(false);
        Timer.StopTimer();
    }
}
