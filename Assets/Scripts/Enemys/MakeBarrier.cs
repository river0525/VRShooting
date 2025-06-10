using UnityEngine;

public class MakeBarrier : MonoBehaviour
{
    [SerializeField] GameObject barrier;
    [SerializeField] OVRScreenFade fade;
    [SerializeField] Crystal crystal;
    [SerializeField] AudioSource _audio;

    const int barrierSE = 15;
    const string isProtectedFlag = "ÉoÉäÉAìWäJçœÇ›";

    private bool isProtected = false;
    private Vector3 startPlayerPos;
    private EnemyBase enemyBase;
    private GameObject player;
    private PlayerMover playerMover;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        player = GameObject.FindWithTag("Player");
        playerMover = player.GetComponent<PlayerMover>();
        barrier.SetActive(false);
        startPlayerPos = PlayerMover.GetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if ((float)enemyBase.hp.Get() / enemyBase.hp.GetMax() > 0.1f) return;
        if (isProtected) return;
        isProtected = true;
        FlagDataBase.Instance.SetFlag(isProtectedFlag, true);
        crystal.MakeBarrier();
        fade.FadeIn();
        playerMover.StartCoroutine("SetPosition", startPlayerPos);
        barrier.SetActive(true);
        _audio.PlayOneShot(SEDataBase.Instance.GetSE(barrierSE));
    }
}
