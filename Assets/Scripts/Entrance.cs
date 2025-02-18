using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    [SerializeField] string changeSceneName;
    [SerializeField] AudioClip DoorSE;
    public static bool canEnter = true;
    private OVRScreenFade screenFade;
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
        if (other.gameObject.tag == "Player" && OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch) && canEnter)
        {
            PlayerMover.canMove = false;
            canEnter = false;
            AudioManager.instance.PlaySE(DoorSE);
            sceneLoader.LoadScene(changeSceneName);
        }
    }
    
}
