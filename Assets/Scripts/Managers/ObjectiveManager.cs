using System.Reflection.Emit;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance = null;

    public string CurrentObjective { get; private set; }

    private ObjectiveDataBase database;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        database = Resources.Load<ObjectiveDataBase>("ObjectiveDataBase");
        UpdateObjective();
    }
    
    public void UpdateObjective()
    {
        CurrentObjective = "";
        foreach(var data in database.Data)
        {
            foreach (var flag in data.CompletionFlagKey)
            {
                if (FlagDataBase.Instance.GetFlag(flag.key) != flag.value) goto Label;
            }
            CurrentObjective = data.Description;
            break;
        Label:;
        }
    }
}
