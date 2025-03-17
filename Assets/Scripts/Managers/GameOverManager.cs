using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] AudioClip inputGameOverSE;
    [SerializeField] AudioClip inputRetrySE;
    [SerializeField] GameObject inputGameOverText;
    private static AudioClip gameOverSE;
    private static AudioClip retrySE;
    private static GameObject gameOverText;
    private SceneLoader sceneLoader;
    private bool isGameOver = false;
    private PlayerStatus playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        gameOverSE = inputGameOverSE;
        retrySE = inputRetrySE;
        gameOverText = inputGameOverText;
        gameOverText.SetActive(false);
        var playerObj = GameObject.FindWithTag("Player");
        playerStatus = playerObj.GetComponent<PlayerStatus>();
        var sceneLoaderObj = GameObject.FindWithTag("SceneLoader");
        sceneLoader = sceneLoaderObj.GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStatus.GetHP() == 0 && !isGameOver)
        {
            isGameOver = true;
            GameOver();
        }
    }


    public static void GameOver()
    {
        Entrance.canEnter = false;
        PlayerMover.canMove = false;
        gameOverText.SetActive(true);
        AudioManager.instance.PlaySE(gameOverSE);
    }
    public void Retry()
    {
        if (!isGameOver) return;
        playerStatus.FullRecover();
        AudioManager.instance.PlaySE(retrySE);
        sceneLoader.LoadScene(SceneManager.GetActiveScene().name);
    }
}
