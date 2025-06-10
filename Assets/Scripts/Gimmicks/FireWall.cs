using UnityEngine;

public class FireWall : MonoBehaviour, IGimmickBooter, IRemovableGimmickOnStart
{
    const int evaporationSE = 7;
    public void RemoveOnStart()
    {
        Destroy(gameObject);
    }
    public bool TryBoot()
    {
        AudioManager.instance.PlaySE(evaporationSE);
        Destroy(gameObject);
        return true;
    }
}