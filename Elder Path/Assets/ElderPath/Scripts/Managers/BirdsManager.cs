using UnityEngine;

public class BirdsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private Bird birdPrefab;

    [Header("Configuration")]
    [SerializeField] private float outsideCameraDistance = 2f;
    [SerializeField] private float spawnInterval = 2f;

    private float timer;

    private void Awake()
    {
        if (!cam) cam = Camera.main;
        timer = spawnInterval;
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
        float xSpawn = spawnOnLeft ? -cam.GetWidth() / 2 - outsideCameraDistance : cam.GetWidth() / 2 + outsideCameraDistance;
        float ySpawn = Random.Range(-cam.GetHeight() / 2, cam.GetHeight() / 2);
        return new Vector2(xSpawn, ySpawn);
    }

    private Vector2 GetTargetPoint(bool spawnOnLeft)
    {
        float xTarget = spawnOnLeft ? cam.GetWidth() / 2 + outsideCameraDistance : -cam.GetWidth() / 2 - outsideCameraDistance;
        float yTarget = Random.Range(-cam.GetHeight() / 2, cam.GetHeight() / 2);
        return new Vector2(xTarget, yTarget);
    }
}