using UnityEngine;

public class BirdsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private Bird birdPrefab;

    [Header("Configuration")]
    [SerializeField] private float outsideCameraDistance = 2f;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float despawnBuffer = 5f;

    private float timer;

    private void Awake()
    {
        if (!cam) cam = Camera.main;
        timer = spawnInterval;
    }

    private void Start()
    {
        SpawnBird();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnBird();
            timer = spawnInterval;
        }
    }

    private void SpawnBird()
    {
        bool spawnOnLeft = Random.value > 0.5f;
        Bird bird = Instantiate(birdPrefab, GetSpawnPoint(spawnOnLeft), Quaternion.identity, transform);
        bird.SetFinalPoint(GetTargetPoint(spawnOnLeft));
    }

    private Vector2 GetSpawnPoint(bool spawnOnLeft)
    {
        Vector3 cameraPosition = cam.transform.position;
        float xSpawn = spawnOnLeft ? cameraPosition.x - cam.GetWidth() / 2 - outsideCameraDistance
                                   : cameraPosition.x + cam.GetWidth() / 2 + outsideCameraDistance;
        float ySpawn = Random.Range(cameraPosition.y - cam.GetHeight() / 2, cameraPosition.y + cam.GetHeight() / 2);
        return new Vector2(xSpawn, ySpawn);
    }

    private Vector2 GetTargetPoint(bool spawnOnLeft)
    {
        Vector3 cameraPosition = cam.transform.position;
        float xTarget = spawnOnLeft ? cameraPosition.x + cam.GetWidth() / 2 + despawnBuffer
                                    : cameraPosition.x - cam.GetWidth() / 2 - despawnBuffer;
        float yTarget = Random.Range(cameraPosition.y - cam.GetHeight() / 2 - despawnBuffer,
                                     cameraPosition.y + cam.GetHeight() / 2 + despawnBuffer);
        return new Vector2(xTarget, yTarget);
    }
}