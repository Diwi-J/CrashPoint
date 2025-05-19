using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform Target;  
    public float SmoothSpeed = 5f;
    public Vector3 Offset = new Vector3(0, 0, -10f);  

    void LateUpdate()
    {
        if (Target == null) return;

        Vector3 TargetPosition = Target.position + Offset;
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, TargetPosition, SmoothSpeed * Time.deltaTime);
        transform.position = SmoothedPosition;
    }
}
