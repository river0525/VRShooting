using UnityEngine;

public abstract class SearchableObj : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Reset()
    {
        gameObject.tag = "Searchable";
        if(GetComponent<Rigidbody>() == null ) gameObject.AddComponent<Rigidbody>();
        if (GetComponent<Collider>() == null) Debug.Log("トリガーをオンにしたコライダーをアタッチしてください");
        if (transform.Find("SearchableMark") == null) Debug.Log("ビックリマークを子オブジェクトにしてください");
    }

    // Update is called once per frame
    public abstract void Searched();
}
