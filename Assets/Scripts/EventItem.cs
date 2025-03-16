using UnityEngine;

public class EventItem : MonoBehaviour
{
    [SerializeField] private FlagManager.FlagName flagName;
    [SerializeField] private string nextPurpose;

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
        Destroy(gameObject);
    }
}
