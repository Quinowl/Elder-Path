using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField, Range(0f,0.5f)] private float speedFactor = 0.06f;
    [SerializeField] private Renderer rend;
    [SerializeField] private Camera cam;
    private Vector2 position = Vector2.zero;
    private Vector2 cameraOldPosition;

    private void Awake() 
    {
        if (!cam) cam = Camera.main;
        if (!rend) rend = GetComponent<Renderer>();
    }

    private void Start() 
    {
        cameraOldPosition = cam.transform.position;
        Vector2 backgroundHalfSize = new Vector2(cam.orthographicSize * Screen.width / Screen.height, cam.orthographicSize);
        transform.localScale = new Vector3(backgroundHalfSize.x * 2, backgroundHalfSize.y * 2, transform.localScale.z);
        rend.material.SetTextureScale(Constants.MATERIAL_PROPERTY_MAIN_TEXTURE, backgroundHalfSize);
    }   

    private void Update() 
    {
        Vector2 camVariation = new Vector2(cam.transform.position.x - cameraOldPosition.x, cam.transform.position.y - cameraOldPosition.y);
        position.Set(position.x + camVariation.x * speedFactor, position.y + camVariation.y * speedFactor);
        rend.material.SetTextureOffset(Constants.MATERIAL_PROPERTY_MAIN_TEXTURE, position);
        cameraOldPosition = cam.transform.position;
    }
}