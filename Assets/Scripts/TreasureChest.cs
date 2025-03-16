using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] GameObject gameClearText;

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        gameClearText.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (!OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) return;
        anim.SetBool("isOpened", true);
        gameClearText.SetActive(true);
    }
}
