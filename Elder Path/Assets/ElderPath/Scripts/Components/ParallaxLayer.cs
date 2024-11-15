using UnityEngine;

public class ParallaxLayer : MonoBehaviour {
    [SerializeField, Range(0f,0.5f)] private float speedFactor = 0.06f;
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY;
    [SerializeField] private Renderer rend;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    private Vector2 position = Vector2.zero;
    private Vector2 targetOldPosition;

    private void Awake() {
        CheckReferences();
    }

    private void Start() {
        InitializeScales();
    }   

    private void Update() {
        MoveLayer();
    }

    private void CheckReferences() {
        if (!cam) cam = Camera.main;
        if (!target) Debug.LogError($"Parallax layer {name} has not target asigned");
        if (!rend) rend = GetComponent<Renderer>();
    }

    private void InitializeScales() {
        targetOldPosition = target.position;
        Vector2 backgroundHalfSize = new Vector2(cam.orthographicSize * Screen.width / Screen.height, cam.orthographicSize);
        transform.localScale = new Vector3(backgroundHalfSize.x * 2, backgroundHalfSize.y * 2, transform.localScale.z);
        rend.material.SetTextureScale(Constants.MATERIAL_PROPERTY_MAIN_TEXTURE, backgroundHalfSize);
    }

    private void MoveLayer() {
        Vector2 posVariation = new Vector2(followX ? target.position.x - targetOldPosition.x : 0f, followY ? target.position.y - targetOldPosition.y : 0);
        position.Set(position.x + posVariation.x * speedFactor, position.y + posVariation.y * speedFactor);
        rend.material.SetTextureOffset(Constants.MATERIAL_PROPERTY_MAIN_TEXTURE, position);
        targetOldPosition = target.position;
    }
}