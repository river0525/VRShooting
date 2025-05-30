using UnityEngine;

public class EventItem : MonoBehaviour
{
    [SerializeField] private string getFlag;

    const int getItemSE = 9;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(FlagDataBase.Instance.GetFlag(getFlag)) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (!OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) return;
        FlagDataBase.Instance.SetFlag(getFlag, true);
        AudioManager.instance.PlaySE(getItemSE);
        Destroy(gameObject);
    }
}
