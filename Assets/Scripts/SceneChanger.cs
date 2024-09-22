using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string changeSceneName;
    [SerializeField] FadeImage fade;

    private bool firstPush = false;
    private bool goNextScene = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene(changeSceneName);
            goNextScene = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown("ChangeScene") && !firstPush)
        {
            fade.StartFadeOut();
            firstPush = true;
        }
    }
}
