using UnityEngine;

public class CharacterHitEffect : MonoBehaviour {

    [SerializeField] private float lifeTime = 0.1f;

    private PlayerController playerController;
    private float spawnTime;

    public void Initialize(PlayerController playerController) {
        this.playerController = playerController;
        spawnTime = Time.time;
    }

    private void Update() {
        if (Time.time >= (spawnTime + lifeTime)) playerController.HitEffectPool.ReturnToPool(this);
    }
}