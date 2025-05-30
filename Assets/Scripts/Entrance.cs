using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    public static bool canEnter = true;
    private string changeSceneName = "BossRoom";
    private SceneLoader sceneLoader;

    const int DoorSE = 20;
    const int cannotOpenSE = 10;
    const string searchedFlag = "ドアを調べた";
    const string getKeyFlag = "カギを手に入れた";
    const string openedFlag = "ドアを開けた";

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
        FlagDataBase.Instance.SetFlag(searchedFlag, true);
        if (!canEnter) return;
        if (!FlagDataBase.Instance.GetFlag(getKeyFlag))
        {
            AudioManager.instance.PlaySE(cannotOpenSE);
            return;
        }
        FlagDataBase.Instance.SetFlag(openedFlag, true);
        PlayerMover.canMove = false;
        canEnter = false;
        AudioManager.instance.PlaySE(DoorSE);
        sceneLoader.LoadScene(changeSceneName);
    }
    
}
