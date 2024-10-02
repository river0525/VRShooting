using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject menuObj;
    [SerializeField] GameObject cmVcam1;
    [SerializeField] AudioClip openMenuSE;
    [SerializeField] AudioClip closeMenuSE;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    private bool menuOpened = false;

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
        if (Input.GetButtonDown("Menu"))
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
        Enemy.canMove = false;
        AudioManager.instance.PlaySE(openMenuSE);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cmVcam1.SetActive(false);
    }

    public void CloseMenu()
    {
        if (!menuOpened) return;
        menuOpened = false;
        menuObj.SetActive(false);
        PlayerMover.canMove = true;
        Enemy.canMove = true;
        AudioManager.instance.PlaySE(closeMenuSE);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cmVcam1.SetActive(true);
    }

    public void GoTitle()
    {
        //É^ÉCÉgÉãâÊñ Ç÷ñﬂÇÈèàóùÇèëÇ≠
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
