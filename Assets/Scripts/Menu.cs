using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject menuObj;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    const int openMenuSE = 11;
    const int closeMenuSE = 2;
    const string firstSceneName = "Dungeon_5";

    private bool menuOpened = false;
    private GameObject[] enemys;

    private void Start()
    {
        menuObj.SetActive(false);
        //BGM
        audioMixer.GetFloat("BGM", out float bgmVolume);
        BGMSlider.value = bgmVolume;
        //SE
        audioMixer.GetFloat("SE", out float seVolume);
        SESlider.value = seVolume;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start, OVRInput.Controller.LTouch))
        {
            if (menuOpened) CloseMenu();
            else OpenMenu();
        }
    }
    void OpenMenu()
    {
        if (menuOpened) return;
        menuOpened = true;
        menuObj.SetActive(true);
        PlayerMover.canMove = false;
        Timer.Pause();
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemys)
        {
            var enemyBase = enemy.GetComponent<EnemyBase>();
            enemyBase.Pause();
        }
        AudioManager.instance.PlaySE(openMenuSE);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMenu()
    {
        if (!menuOpened) return;
        menuOpened = false;
        menuObj.SetActive(false);
        PlayerMover.canMove = true;
        Timer.Restart();
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemys)
        {
            var enemyBase = enemy.GetComponent<EnemyBase>();
            enemyBase.Restart();
        }
        AudioManager.instance.PlaySE(closeMenuSE);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResetGame()
    {
        PlayerStatus.Reset();
        FlagDataBase.Instance.ResetFlag();
        SceneManager.LoadScene(firstSceneName);
    }

    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SE", volume);
    }
}
