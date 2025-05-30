using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] Color safetyColor;
    [SerializeField] Color dangerousColor;

    private float dangerousSeconds = 10f;
    private TextMeshProUGUI text;

    private static float countdownSeconds = 0f;
    private static bool isStopped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped) return;
        if (countdownSeconds == 0f)
        {
            text.text = "";
            return;
        }
        countdownSeconds -= Time.deltaTime;
        if (countdownSeconds < 0f) countdownSeconds = 0f;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        text.text = "Žc‚è"+span.ToString(@"mm\:ss");
        if (countdownSeconds > dangerousSeconds) text.color = safetyColor;
        else text.color = dangerousColor;
    }

    public static void Pause()
    {
        isStopped = true;
    }
    public static void Restart()
    {
        isStopped = false;
    }

    public static void SetCounter(float minutes)
    {
        countdownSeconds = minutes * 60;
        isStopped = false;
    }

    public static void ResetCounter()
    {
        countdownSeconds = 0;
    }

    public static bool IsTime()
    {
        return countdownSeconds == 0f;
    }
}
