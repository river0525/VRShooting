using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectiveDataBase", menuName = "Scriptable Objects/ObjectiveDataBase")]
public class ObjectiveDataBase : ScriptableObject
{
    [Serializable]
    public class ObjectiveData
    {
        [Header("表示テキスト")][field: SerializeField, TextArea] public string Description { get; private set; }

        [Header("表示条件")][field: SerializeField] public Flag[] CompletionFlagKey { get; private set; }
    }
    [field: SerializeField] public ObjectiveData[] Data { get; private set; }
}
