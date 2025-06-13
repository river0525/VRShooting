using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    [SerializeField] public string defaultStateName;

    private const string tutorialControllerTag = "TutorialController";

    private UniqueQueue<string> doing;
    private HashSet<string> done;
    private Dictionary<string, string> explanations;
    private Animator anim;
    private TextMeshProUGUI text;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            Initialize();
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        var obj = GameObject.Find(tutorialControllerTag);
        anim = obj.GetComponent<Animator>();
        text = obj.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Initialize()
    {
        doing = new UniqueQueue<string>();
        done = new HashSet<string>();
        explanations = new Dictionary<string, string>();
    }
    private void Update()
    {
        UpdateAnimation();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void DoTutorial(string name, string explanation)
    {
        if (done.Contains(name)) return;
        doing.Enqueue(name);
        explanations[name] = explanation;
    }
    public void DoneTutorial(string name)
    {
        done.Add(name);
    }
    private void UpdateAnimation()
    {
        while(doing.Count > 0)
        {
            var tutorialName = doing.Peek();
            if (done.Contains(tutorialName))
            {
                doing.Dequeue();
                continue;
            }
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName(tutorialName))
            {
                anim.Play(tutorialName);
                text.text = explanations[tutorialName];
            }
            goto Label;
        }
        anim.Play(defaultStateName);
        text.text = "";
    Label:;
    }
}
