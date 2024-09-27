using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    [SerializeField] string changeSceneName;
    public static bool canEnter = true;
    private void Start()
    {
        canEnter = true;
    }
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown("ChangeScene") && canEnter)
        {
            FadeImage.LoadScene(changeSceneName);
        }
    }
}
