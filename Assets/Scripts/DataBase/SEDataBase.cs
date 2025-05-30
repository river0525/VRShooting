using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SEDataBase", menuName = "Scriptable Objects/SEDataBase")]
public class SEDataBase : ScriptableObject
{
    private static SEDataBase instance = null;
    public static SEDataBase Instance
    {
        get
        {
            if (instance == null) instance = Resources.Load<SEDataBase>("SEDataBase");
            return instance; 
        }
    }

    [SerializeField,NonReorderable] private SEData[] seData;

    public AudioClip GetSE(int idx)
    {
        return seData[idx].GetSE();
    }
}
