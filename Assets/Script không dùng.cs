using UnityEngine;
using UnityEngine.InputSystem;

public class DestructibleSprite : MonoBehaviour
{
    private Texture2D texture;
    private SpriteRenderer sr;

    [Header("Destruction Settings")]
    public int radius = 10;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Clone texture so we don't modify original asset
        texture = Instantiate(sr.sprite.texture);

        sr.sprite = Sprite.Create(
            texture,
            sr.sprite.rect,
            new Vector2(0.5f, 0.5f),
            sr.sprite.pixelsPerUnit
        );
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            DestroyAt(worldPos);
        }
    }

void DestroyAt(Vector2 worldPos)
{
    Vector2 localPos = transform.InverseTransformPoint(worldPos);

    Sprite sprite = sr.sprite;

    float pixelsPerUnit = sprite.pixelsPerUnit;
    Vector2 pivot = sprite.pivot;
    Rect rect = sprite.rect;

    int x = Mathf.RoundToInt(pivot.x + localPos.x * pixelsPerUnit);
    int y = Mathf.RoundToInt(pivot.y + localPos.y * pixelsPerUnit);

    // 🔥 ADD THIS OFFSET
    x += (int)rect.x;
    y += (int)rect.y;

    EraseCircle(x, y);
}

    void EraseCircle(int cx, int cy)
    {
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                if (x * x + y * y <= radius * radius)
                {
                    int px = cx + x;
                    int py = cy + y;

                    if (px >= 0 && px < texture.width &&
                        py >= 0 && py < texture.height)
                    {
                        texture.SetPixel(px, py, Color.clear);
                    }
                }
            }
        }

        texture.Apply();

        // Optional: update collider (expensive!)
        UpdateCollider();
    }

    void UpdateCollider()
    {
        var col = GetComponent<PolygonCollider2D>();

        bool success = col.CreateFromSprite(
            sr.sprite,
            0.5f,   // detail (0–1, higher = more accurate)
            1,      // alphaTolerance (LOW = more sensitive)
            true    // hole detection
        );

        if (!success)
        {
            Debug.LogWarning("Collider generation failed!");
        }
    }
}