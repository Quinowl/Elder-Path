using UnityEngine;

public class FinalLevelTrigger : MonoBehaviour
{

    [SerializeField] private Collider2D collider2d;
    private Level currentLevel;

    private void Awake()
    {
        if (!collider2d) collider2d = GetComponent<Collider2D>();
    }

    public void Configure(Level level)
    {
        currentLevel = level;
        collider2d.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) currentLevel.EndLevel();
    }
}
