using UnityEngine;

public class PlaySE : MonoBehaviour
{
    private AudioSource _audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void Play(int index)
    {
        _audio.PlayOneShot(SEDataBase.Instance.GetSE(index));
    }
}
