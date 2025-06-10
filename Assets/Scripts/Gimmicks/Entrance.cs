using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour,IGimmickBooter
{
    public static bool canEnter = true;
    private string changeSceneName = "BossRoom";
    private SceneLoader sceneLoader;

    const int DoorSE = 20;

    private void Start()
    {
        canEnter = true;
        var sceneLoaderObj = GameObject.FindWithTag("SceneLoader");
        sceneLoader = sceneLoaderObj.GetComponent<SceneLoader>();
    }
    public bool TryBoot()
    {
        if (!canEnter) return false;
        PlayerMover.canMove = false;
        canEnter = false;
        AudioManager.instance.PlaySE(DoorSE);
        sceneLoader.LoadScene(changeSceneName);
        return true;
    }
}
