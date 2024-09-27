using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadeImage : MonoBehaviour
{
    [Header("最初からフェードインが完了しているかどうか")] [SerializeField] bool firstFadeInComp;
    private static Image img;
    private static int frameCount = 0;
    private static float timer = 0.0f;
    private static bool fadeIn = false;
    private static bool fadeOut = false;
    private static bool compFadeIn = false;
    private static bool compFadeOut = false;
    private static bool firstPush = false;
    private static bool goNextScene = false;
    private static string changeSceneName;

    /// <summary>
    /// フェードインを開始する
    /// </summary>
    public void StartFadeIn()
    {
        if (fadeIn || fadeOut)
        {
            return;
        }
        fadeIn = true;
        compFadeIn = false;
        timer = 0.0f;
        img.color = new Color(1, 1, 1, 1);
        img.fillAmount = 1;
        img.raycastTarget = true;
    }

    /// <summary>
    /// フェードインが完了したかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsFadeInComplete()
    {
        return compFadeIn;
    }

    /// <summary>
    /// フェードアウトを開始する
    /// </summary>
    private static void StartFadeOut()
    {
        if (fadeIn || fadeOut)
        {
            return;
        }
        fadeOut = true;
        compFadeOut = false;
        timer = 0.0f;
        img.color = new Color(1, 1, 1, 0);
        img.fillAmount = 0;
        img.raycastTarget = true;
    }

    /// <summary>
    /// フェードアウトを完了したかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsFadeOutComplete()
    {
        return compFadeOut;
    }

    void Start()
    {
        img = GetComponent<Image>();
        frameCount = 0;
        timer = 0.0f;
        fadeIn = false;
        fadeOut = false;
        compFadeIn = false;
        compFadeOut = false;
        firstPush = false;
        goNextScene = false;
        changeSceneName = "";
        if (firstFadeInComp)
        {
            FadeInComplete();
        }
        else
        {
            StartFadeIn();
        }
    }

    void Update()
    {
        //シーン移行時の処理の重さでTime.deltaTimeが大きくなってしまうから2フレーム待つ
        if (frameCount > 2)
        {
            if (fadeIn)
            {
                FadeInUpdate();
            }
            else if (fadeOut)
            {
                FadeOutUpdate();
            }
        }
        ++frameCount;
        if (!goNextScene && IsFadeOutComplete())
        {
            SceneManager.LoadScene(changeSceneName);
            goNextScene = true;
        }
    }

    //フェードイン中
    private void FadeInUpdate()
    {
        if (timer < 1f)
        {
            img.color = new Color(1, 1, 1, 1 - timer);
            img.fillAmount = 1 - timer;
        }
        else
        {
            FadeInComplete();
        }
        timer += Time.deltaTime;
    }

    //フェードアウト中
    private void FadeOutUpdate()
    {
        if (timer < 1f)
        {
            img.color = new Color(1, 1, 1, timer);
            img.fillAmount = timer;
        }
        else
        {
            FadeOutComplete();
        }
        timer += Time.deltaTime;
    }

    //フェードイン完了
    private void FadeInComplete()
    {
        img.color = new Color(1, 1, 1, 0);
        img.fillAmount = 0;
        img.raycastTarget = false;
        timer = 0.0f;
        fadeIn = false;
        compFadeIn = true;
    }

    //フェードアウト完了
    private void FadeOutComplete()
    {
        img.color = new Color(1, 1, 1, 1);
        img.fillAmount = 1;
        img.raycastTarget = false;
        timer = 0.0f;
        fadeOut = false;
        compFadeOut = true;
    }

    public static void LoadScene(string sceneName)
    {
        if (firstPush) return;
        StartFadeOut();
        firstPush = true;
        changeSceneName = sceneName;
    }
}
