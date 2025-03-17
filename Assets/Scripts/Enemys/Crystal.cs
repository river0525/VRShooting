using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    [SerializeField] private int maxHP = 3;
    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject barrier;

    private HP hp;

    // Start is called before the first frame update
    void Start()
    {
        var nowHP = maxHP;
        hp = new HP(nowHP, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = (float)hp.Get() / hp.GetMax();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "PlayerAttack") return;
        var damage = 1;
        hp.Subtract(damage);
        if (hp.Get() != 0) return;
        PlayerStatus.SetPurpose("É{ÉXÇÇΩÇ®ÇπÅI");
        Destroy(barrier);
        Destroy(gameObject);
    }
}
