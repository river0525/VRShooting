using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;//StartÇÊÇËëOÇ…é¿çsÇ≥ÇÍÇÈÇÃÇ≈ÅAAwakeÇ≈ìoò^
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    [SerializeField] int gameOverSE;
    [SerializeField] int retrySE;
    private GameObject gameOverText;
    private bool isGameOver = false;
    private SceneLoader sceneLoader;
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        gameOverText = GameObject.FindWithTag("GameOverText");
        gameOverText.SetActive(false);
        isGameOver = false;
        var sceneLoaderObj = GameObject.FindWithTag("SceneLoader");
        sceneLoader = sceneLoaderObj.GetComponent<SceneLoader>();
    }
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Entrance.canEnter = false;
        PlayerMover.canMove = false;
        gameOverText.SetActive(true);
        AudioManager.instance.PlaySE(gameOverSE);
    }
    public void Retry()
    {
        if (!isGameOver) return;
        PlayerManager.instance.FullRecover();
        AudioManager.instance.PlaySE(retrySE);
        sceneLoader.LoadScene(SceneManager.GetActiveScene().name);
    }
}
