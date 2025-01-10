using UnityEngine;

public class CameraRig : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform target;

    [Header("Configuration")]
    [SerializeField] private float followSpeed = 2f;

    private bool canMove = false;
    private float halfCameraWidth;
    private float halfCameraHeight;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private void Awake()
    {
        if (!mainCamera) mainCamera = Camera.main;
        if (!target) Debug.LogError("Camera rig has no target associated.");

        CalculateCameraDimensions();
    }

    private void FixedUpdate()
    {
        if (!canMove || !target) return;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x + halfCameraWidth, maxBounds.x - halfCameraWidth);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y + halfCameraHeight, maxBounds.y - halfCameraHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void Configure(bool canMove, Vector2 minBounds, Vector2 maxBounds)
    {
        this.canMove = canMove;
        this.minBounds = minBounds;
        this.maxBounds = maxBounds;

        CalculateCameraDimensions();
    }

    public void UpdateBounds(Vector2 newMinBounds, Vector2 newMaxBounds)
    {
        minBounds = newMinBounds;
        maxBounds = newMaxBounds;
    }

    private void CalculateCameraDimensions()
    {
        halfCameraHeight = mainCamera.orthographicSize;
        halfCameraWidth = halfCameraHeight * mainCamera.aspect;
    }
}