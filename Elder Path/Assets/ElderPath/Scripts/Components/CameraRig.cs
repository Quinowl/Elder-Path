using UnityEngine;

public class CameraRig : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform target;

    [Header("Configuration")]
    [SerializeField] private float followSpeed = 2f;

    private bool canMove;
    private float halfCameraWidth;
    private float halfCameraHeight;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private void Awake()
    {
        if (!mainCamera) mainCamera = Camera.main;
        if (!target) Debug.LogError("Camera rig has no target associated.");
        halfCameraHeight = mainCamera.orthographicSize;
        halfCameraWidth = halfCameraHeight * Screen.width / Screen.height;
    }

    private void FixedUpdate()
    {
        if (!canMove) return;
        if (!target) return;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
        float xClamp = Mathf.Clamp(smoothedPosition.x, minBounds.x + halfCameraWidth, maxBounds.x - halfCameraWidth);
        float yClamp = Mathf.Clamp(smoothedPosition.y, minBounds.y + halfCameraHeight, maxBounds.y - halfCameraHeight);
        transform.position = new Vector3(xClamp, yClamp, transform.position.z);
    }

    public void Configure(bool canMove, Vector2 minBounds, Vector2 maxBounds)
    {
        this.canMove = canMove;
        this.minBounds = minBounds;
        this.maxBounds = maxBounds;
    }
}