using UnityEngine;

public class MakeBarrier : MonoBehaviour
{
    [SerializeField] GameObject barrier;
    [SerializeField] OVRScreenFade fade;
    [SerializeField] Crystal crystal;
    [SerializeField] AudioClip barrierSE;
    [SerializeField] AudioSource audio;

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
        PlayerStatus.SetPurpose("クリスタルを攻撃し\nバリアをこわせ！");
        crystal.MakeBarrier();
        fade.FadeIn();
        playerMover.StartCoroutine("SetPosition", startPlayerPos);
        barrier.SetActive(true);
        audio.PlayOneShot(barrierSE);
    }
}
