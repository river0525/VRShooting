using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private OVRScreenFade screenFade;
    private void Start()
    {
        
    }
    public void LoadScene(string changeSceneName)
    {
        screenFade.FadeOut();
        StartCoroutine(LoadScene(screenFade.fadeTime, changeSceneName));
    }
    IEnumerator LoadScene(float delay, string changeSceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(changeSceneName);
    }
}
