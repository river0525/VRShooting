using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    [SerializeField] AudioClip DoorSE;
    [SerializeField] AudioClip cannotOpenSE;
    public static bool canEnter = true;
    private string changeSceneName = "BossRoom";
    private SceneLoader sceneLoader;

    private void Start()
    {
        canEnter = true;
        var sceneLoaderObj = GameObject.FindWithTag("SceneLoader");
        sceneLoader = sceneLoaderObj.GetComponent<SceneLoader>();
    }
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (!OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) return;
        if (!canEnter) return;
        if (!FlagManager.CheckFlag(FlagManager.FlagName.getKey))
        {
            PlayerStatus.SetPurpose("カギを手に入れろ！");
            AudioManager.instance.PlaySE(cannotOpenSE);
            return;
        }
        PlayerMover.canMove = false;
        canEnter = false;
        AudioManager.instance.PlaySE(DoorSE);
        PlayerStatus.SetPurpose("ボスをたおせ！");
        sceneLoader.LoadScene(changeSceneName);
    }
    
}
