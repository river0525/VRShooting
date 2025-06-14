using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    [SerializeField] private int maxHP = 3;
    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject barrier;
    [SerializeField] private GameObject _light;
    [SerializeField] private GameObject nomalTower;
    [SerializeField] private GameObject brokenTower;
    [SerializeField] private GameObject canvas;

    const int breakSE = 1;
    const string breakFlag = "ƒNƒŠƒXƒ^ƒ‹‚ð‰ó‚µ‚½";

    private bool canDamage = false;
    private HP hp;
    private AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        canDamage = false;
        var nowHP = maxHP;
        hp = new HP(nowHP, maxHP);
        nomalTower.SetActive(true);
        _light.SetActive(false);
        brokenTower.SetActive(false);
        canvas.SetActive(false);
        _audio = GetComponent<AudioSource>();
        FlagDataBase.Instance.SetFlag(breakFlag, false);
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
        _light.SetActive(true);
        canvas.SetActive(true);
        canDamage = true;
    }

    public void Break()
    {
        nomalTower.SetActive(false);
        _light.SetActive(false);
        brokenTower.SetActive(true);
        _audio.PlayOneShot(SEDataBase.Instance.GetSE(breakSE));
        FlagDataBase.Instance.SetFlag(breakFlag,true);
        Destroy(barrier);
    }
}
