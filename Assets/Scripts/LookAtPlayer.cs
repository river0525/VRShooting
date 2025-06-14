using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private const float playerHeight = 2f;
    void Update()
    {
        // １）砲身の位置からターゲットへの方向ベクトル
        Vector3 dir = PlayerMover.GetPosition()  + Vector3.up * playerHeight - transform.position;

        // ２）水平距離と高低差からピッチ角度を計算
        float horizontalDist = new Vector3(dir.x, 0f, dir.z).magnitude;
        // Atan2(高低差, 水平距離) → ラジアン
        float targetPitch = Mathf.Atan2(dir.y, horizontalDist) * Mathf.Rad2Deg;

        // ４）ローカル X 軸だけ回転を更新
        transform.localEulerAngles = new Vector3(-targetPitch, 0f, 0f);
    }
}
