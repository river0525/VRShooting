using UnityEngine;

public class OVRSnapTurn : MonoBehaviour
{
    public float turnSpeed = 45f; // 回転速度（°/秒）
    private Transform trackingSpace; // OVRCameraRig の TrackingSpace
    private bool stickDown = false;

    void Start()
    {
        // OVRCameraRig の TrackingSpace を取得
        trackingSpace = GameObject.Find("OVRCameraRig/TrackingSpace").transform;
    }

    void Update()
    {
        if (!PlayerMover.canMove) return;
        // 右スティックのX軸入力（左右の回転）
        float turnInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;
        
        // スティックを倒しているときに回転
        if (Mathf.Abs(turnInput) > 0.1f)
        {
            if (stickDown) return;
            if (turnInput < 0) turnInput = -1;
            if (turnInput > 0) turnInput = 1;
            trackingSpace.Rotate(Vector3.up, turnInput * turnSpeed);
            stickDown = true;
            return;
        }
        stickDown = false;
    }
}
