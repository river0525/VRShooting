using UnityEngine;

public class PlaySE : MonoBehaviour
{
    [SerializeField] public AudioClip[] SE;

    private AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Play(int index)
    {
        audio.PlayOneShot(SE[index]);
    }
}
