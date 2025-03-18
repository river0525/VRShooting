using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] int countdownMinutes = 3;
    [SerializeField] Color safetyColor;
    [SerializeField] Color dangerousColor;

    private float countdownSeconds;
    private float dangerousSeconds = 60f;
    private TextMeshProUGUI text;

    private static bool isStopped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        countdownSeconds = countdownMinutes * 60;
        isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped) return;
        countdownSeconds -= Time.deltaTime;
        if (countdownSeconds < 0f) countdownSeconds = 0f;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        text.text = span.ToString(@"mm\:ss");
        if (countdownSeconds > dangerousSeconds) text.color = safetyColor;
        else text.color = dangerousColor;
        if (countdownSeconds <= 0) GameOverManager.GameOver();
    }

    public static void StopTimer()
    {
        isStopped = true;
    }
    public static void StartTimer()
    {
        isStopped = false;
    }
}
