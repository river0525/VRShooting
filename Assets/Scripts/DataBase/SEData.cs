using UnityEngine;

[CreateAssetMenu(fileName = "SEData", menuName = "Scriptable Objects/SEData")]
public class SEData : ScriptableObject
{
    [SerializeField] private AudioClip[] SE;
    public AudioClip GetSE()
    {
        int idx = Random.Range(0, SE.Length);
        return SE[idx];
    }
}
