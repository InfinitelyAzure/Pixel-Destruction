using UnityEngine;

public class EXPZone : MonoBehaviour
{
    public GameObject EXPPrefab;

    [SerializeField] private int EXPAmount;
    private int expPerVisual = 15; // hiện 1 particle mỗi 15 exp
    public float lifetime = 1f;

    private int visualAccumulator = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Chunk chunk)) return;

        SpawnEXP(chunk);
    }

    void SpawnEXP(Chunk chunk)
    {
        Transform t = chunk.transform;
        Rigidbody2D oldRb = chunk.GetComponent<Rigidbody2D>();

        LevelSystem.Instance.AddEXP(EXPAmount);
        visualAccumulator += EXPAmount;

        int spawnCount = visualAccumulator / expPerVisual;

        visualAccumulator %= expPerVisual;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 0.3f;
            Vector2 spawnPos = (Vector2)t.position + offset;

            GameObject newObj = Instantiate(EXPPrefab, spawnPos, t.rotation);

            if (oldRb != null && newObj.TryGetComponent(out Rigidbody2D newRb))
            {
                newRb.linearVelocity = oldRb.linearVelocity;
                newRb.angularVelocity = oldRb.angularVelocity;
            }

            Destroy(newObj, lifetime);
        }

        Destroy(chunk.gameObject);
    }
}