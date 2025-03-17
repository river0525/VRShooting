using UnityEngine;

public class EventItem : MonoBehaviour
{
    [SerializeField] private FlagManager.FlagName flagName;
    [SerializeField] private string nextPurpose;
    [SerializeField] private AudioClip getItemSE;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (!OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) return;
        FlagManager.EnableFlag(flagName);
        PlayerStatus.SetPurpose(nextPurpose);
        AudioManager.instance.PlaySE(getItemSE);
        Destroy(gameObject);
    }
}
