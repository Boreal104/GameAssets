using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Camera;
    public float SpeedMultiplier = 5f;
    public float Accuracy = 0.1f;

    private Vector3 PositionOffset;
    private Transform CameraTransform;
    private Transform PlayerTransform;
    private Rigidbody PlayerRigidbody;
    private Vector3 TargetPosition;

    void Start()
    {
        if (Camera == null)
        {
            Debug.LogError("Camera is not assigned!");
            return;
        }

        CameraTransform = Camera.transform;
        PlayerTransform = transform;
        PlayerRigidbody = GetComponent<Rigidbody>();

        if (PlayerRigidbody == null)
        {
            Debug.LogError("Rigidbody not found on player!");
            return;
        }

        PositionOffset = CameraTransform.position - PlayerTransform.position;
        TargetPosition = CameraTransform.position;
    }

    void LateUpdate()
    {
        if (PlayerRigidbody == null || CameraTransform == null) return;

        float Speed = PlayerRigidbody.linearVelocity.magnitude;

        // Update target position even when player stops
        TargetPosition = PlayerTransform.position + PositionOffset;

        Vector3 CameraPosition = CameraTransform.position;
        Vector3 Difference = TargetPosition - CameraPosition;

        if (Difference.magnitude <= Accuracy)
        {
            CameraTransform.position = TargetPosition;
        }
        else
        {
            float LerpFactor = Time.deltaTime * SpeedMultiplier * Mathf.Clamp(Speed, 0.1f, 5f);
            CameraTransform.position = Vector3.Lerp(CameraPosition, TargetPosition, LerpFactor);
        }
    }
}
