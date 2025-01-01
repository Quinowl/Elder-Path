using NaughtyAttributes;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{

    [SerializeField, Range(0f, 0.5f)] private float speedFactor = 0.06f;
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY;
    [SerializeField] private bool autoScroll;
    [SerializeField] private Renderer rend;
    [SerializeField] private Camera cam;
    [SerializeField, HideIf(nameof(autoScroll))] private Transform target;
    private Vector2 position = Vector2.zero;
    private Vector2 targetOldPosition;
    [SerializeField, ShowIf(nameof(autoScroll))] private float autoScrollSpeedX = 0.05f;
    [SerializeField, ShowIf(nameof(autoScroll))] private float autoScrollSpeedY;

    private void Awake()
    {
        CheckReferences();
    }

    private void Start()
    {
        InitializeScales();
    }

    private void Update()
    {
        if (autoScroll) AutoScrollLayer();
        else MoveLayer();
    }

    private void CheckReferences()
    {
        if (!cam) cam = Camera.main;
        if (!target && !autoScroll) Debug.LogError($"Parallax layer {name} has not target asigned");
        if (!rend) rend = GetComponent<Renderer>();
    }

    private void InitializeScales()
    {
        if (target) targetOldPosition = target.position;
        Vector2 textureSize = rend.bounds.size;
        float worldScreenHeight = cam.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight * Screen.width / Screen.height;
        transform.localScale = new Vector3(worldScreenWidth / textureSize.x, worldScreenHeight / textureSize.y, transform.localScale.z);
    }

    private void MoveLayer()
    {
        Vector2 posVariation = new Vector2(followX ? target.position.x - targetOldPosition.x : 0f, followY ? target.position.y - targetOldPosition.y : 0);
        position.Set(position.x + posVariation.x * speedFactor, position.y + posVariation.y * speedFactor);
        rend.material.SetTextureOffset(Constants.MaterialProperties.MAIN_TEXTURE, position);
        targetOldPosition = target.position;
    }

    private void AutoScrollLayer()
    {
        position.Set(position.x + autoScrollSpeedX * speedFactor * Time.deltaTime, position.y + autoScrollSpeedY * speedFactor * Time.deltaTime);
        rend.material.SetTextureOffset(Constants.MaterialProperties.MAIN_TEXTURE, position);
    }
}