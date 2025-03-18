using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    [SerializeField] private int maxHP = 3;
    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject barrier;
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject nomalTower;
    [SerializeField] private GameObject brokenTower;
    [SerializeField] private GameObject canvas;
    [SerializeField] private AudioClip breakSE;

    private bool canDamage = false;
    private HP hp;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        canDamage = false;
        var nowHP = maxHP;
        hp = new HP(nowHP, maxHP);
        nomalTower.SetActive(true);
        light.SetActive(false);
        brokenTower.SetActive(false);
        canvas.SetActive(false);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = (float)hp.Get() / hp.GetMax();
    }
    public void CustomizedOnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "PlayerAttack") return;
        if (!canDamage) return;
        var damage = 1;
        hp.Subtract(damage);
        if (hp.Get() != 0) return;
        Break();
    }
    public void MakeBarrier()
    {
        light.SetActive(true);
        canvas.SetActive(true);
        canDamage = true;
    }

    public void Break()
    {
        nomalTower.SetActive(false);
        light.SetActive(false);
        brokenTower.SetActive(true);
        PlayerStatus.SetPurpose("É{ÉXÇÇΩÇ®ÇπÅI");
        audio.PlayOneShot(breakSE);
        Destroy(barrier);
    }
}
