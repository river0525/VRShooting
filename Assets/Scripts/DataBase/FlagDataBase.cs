using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlagDataBase", menuName = "Scriptable Objects/FlagDataBase")]
public class FlagDataBase : ScriptableObject
{
    private static FlagDataBase instance = null;
    public static FlagDataBase Instance
    {
        get
        {
            if (instance == null) instance = Resources.Load<FlagDataBase>("FlagDataBase");
            return instance;
        }
    }

    [SerializeField] private Flag[] flags;

    private Dictionary<string, bool> _flags;
    private void OnEnable()
    {
        // 設定リストからDictionaryを生成
        _flags = new Dictionary<string, bool>(flags.Length);
        for (int i = 0; i < flags.Length; i++) _flags[flags[i].key] = flags[i].value;
    }

    public bool GetFlag(string key) => _flags.TryGetValue(key, out var v) ? v : false;
    public void SetFlag(string key, bool value)
    {
        if (_flags.ContainsKey(key))
        {
            _flags[key] = value;
            ObjectiveManager.instance.UpdateObjective();
        }
        else Debug.LogWarning($"FlagDatabase: 存在しないキー '{key}' をセットしようとしました");
    }

    public void ResetFlag()
    {
        foreach (var element in flags) _flags[element.key] = false;
        ObjectiveManager.instance.UpdateObjective();
    }
}
