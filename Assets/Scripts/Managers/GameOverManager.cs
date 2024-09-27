using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject inputGameOverImg;
    private static GameObject gameOverImg;
    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOverImg = inputGameOverImg;
        gameOverImg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.PlayerHP == 0 && !isGameOver)
        {
            isGameOver = true;
            GameOver();
        }
        if (isGameOver && Input.GetButtonDown("Retry")) Retry();
    }


    public static void GameOver()
    {
        gameOverImg.SetActive(true);
        Entrance.canEnter = false;
        Player.canMove = false;
        HPSPbar.stopBar = true;
    }
    void Retry()
    {
        GManager.instance.PlayerHP = GManager.instance.maxPlayerHP;
        FadeImage.LoadScene(SceneManager.GetActiveScene().name);
    }
}
