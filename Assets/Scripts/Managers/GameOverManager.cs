using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject inputGameOverImg;
    [SerializeField] AudioClip inputGameOverSE;
    [SerializeField] AudioClip inputRetrySE;
    private static GameObject gameOverImg;
    private static AudioClip gameOverSE;
    private static AudioClip retrySE;
    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOverImg = inputGameOverImg;
        gameOverImg.SetActive(false);
        gameOverSE = inputGameOverSE;
        retrySE = inputRetrySE;
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
        GManager.instance.PlaySE(gameOverSE);
    }
    void Retry()
    {
        GManager.instance.PlayerHP = GManager.instance.maxPlayerHP;
        GManager.instance.PlaySE(retrySE);
        FadeImage.LoadScene(SceneManager.GetActiveScene().name);
    }
}
