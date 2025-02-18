using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private OVRScreenFade screenFade;
    private void Start()
    {
        var playerObj = GameObject.FindWithTag("Player");
        screenFade = playerObj.GetComponent<OVRScreenFade>();
    }
    public void LoadScene(string changeSceneName)
    {
        StartCoroutine(LoadScene(screenFade.fadeTime, changeSceneName));
    }
    IEnumerator LoadScene(float delay, string changeSceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(changeSceneName);
    }
}
