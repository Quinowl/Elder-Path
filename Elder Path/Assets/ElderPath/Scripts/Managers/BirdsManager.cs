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
    }

    private void Update()
    {

    }

    private void SpawnBird()
    {
        bool spawnOnLeft = Random.value > 0.5f;
        Bird bird = Instantiate(birdPrefab, GetSpawnPoint(), Quaternion.identity, transform);
        bird.SetFinalPoint(GetTargetPoint());
    }

    private Vector2 GetSpawnPoint()
    {
        return Vector2.one;
    }

    private Vector2 GetTargetPoint()
    {
        return Vector2.one;
    }
}