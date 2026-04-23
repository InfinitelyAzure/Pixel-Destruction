using UnityEngine;

public class TapToDestroy : MonoBehaviour
{
    [Header("Settings")]
    public float radius;
    public float maxDamage;
    public float minDamage;
    public Camera cam;
    public GameObject VFXPrefab;

    private float defaultRadius = 0.5f;
    private float defaultMaxDamage = 6f;
    private float defaultMinDamage = 3f;

    private void Update()
    {
        HandleMouse();
        HandleTouch();
    }

    // MOUSE INPUT
    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProcessTap(Input.mousePosition);
        }
    }


    //TOUCH INPUT

    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                ProcessTap(t.position);
            }
        }
    }

    public void ResetStats() //gọi từ script chuyển scene
    {
        radius = defaultRadius;
        maxDamage = defaultMaxDamage;
        minDamage = defaultMinDamage;
    }

    void ProcessTap(Vector2 screenPos)
    {
        if (cam == null) cam = Camera.main;

        Vector2 worldPos = cam.ScreenToWorldPoint(screenPos);
        SpawnVFX(worldPos);

        Collider2D[] hits = Physics2D.OverlapCircleAll(worldPos, radius);

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out Chunk chunk)) continue;

            ApplyFalloffDamage(chunk, worldPos);
        }
    }

    void ApplyFalloffDamage(Chunk chunk, Vector2 center)
    {
        float distance = Vector2.Distance(chunk.transform.position, center);

        float t = Mathf.Clamp01(distance / radius);

        float damage = Mathf.Lerp(maxDamage, minDamage, t);

        chunk.TakeDamage(damage);
    }

    void SpawnVFX(Vector2 position)
    {
        if (VFXPrefab == null) return;

        GameObject vfx = Instantiate(VFXPrefab, position, Quaternion.identity);
        vfx.transform.localScale = Vector3.one * radius;
    }



    public void IncreaseRadius()
    {
        radius += 1f;
    }
    public void IncreaseDamage()
    {
        maxDamage += 1.5f;
        minDamage += 1.5f;

    }
}