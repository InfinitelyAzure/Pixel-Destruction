using System.Collections;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float pushForce = 5f;
    private float damagePerSecond;
    public float pulseInterval = 0.3f; 
    public float pulseScale = 1.3f; 
    private float defaultDamage = 5f;
    private Collider2D col;
    private Vector2 originalSize;


    private void OnTriggerEnter2D(Collider2D other)
    {
        FirstImpact(other);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        SustainDMG(other);
    }

    private void Start()
    {
        col = GetComponent<Collider2D>();

        if (col is BoxCollider2D box)
        {
            originalSize = box.size;
        }
        else if (col is CircleCollider2D circle)
        {
            originalSize = new Vector2(circle.radius, 0f);
        }

        //originalSpriteScale = transform.localScale;

        StartCoroutine(PulseCollider());
    }

    void FirstImpact(Collider2D other)
    {
        Chunk chunk = other.GetComponent<Chunk>();
        if (chunk != null)
        {
            chunk.Break();

            Rigidbody2D rb = chunk.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 dir = (rb.position - (Vector2)transform.position).normalized;
                rb.AddForce(dir * pushForce, ForceMode2D.Impulse);
            }
        }
    }

    void SustainDMG(Collider2D other)
    {
        if (!other.TryGetComponent(out Chunk chunk)) return;

        float dmg = damagePerSecond; //* Time.deltaTime;
        chunk.TakeDamage(dmg);
        Rigidbody2D rb = chunk.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 dir = (rb.position - (Vector2)transform.position).normalized;
            rb.AddForce(dir * pushForce, ForceMode2D.Force);
        }
    }

    IEnumerator PulseCollider()
    {
        while (true)
        {
            SetColliderScale(pulseScale);
            yield return new WaitForSeconds(pulseInterval);

            SetColliderScale(1f);
            yield return new WaitForSeconds(pulseInterval);
        }
    }

    void SetColliderScale(float scale)
    {
        if (col is BoxCollider2D box)
        {
            box.size = originalSize * scale;
        }
        else if (col is CircleCollider2D circle)
        {
            circle.radius = originalSize.x * scale;
        }
    }

    public void IncreaseRadius()
    {
        transform.localScale *= 1.3f;
        if (col is BoxCollider2D box)
        {
            box.size *= 1.3f;
            originalSize = box.size;
        }
        else if (col is CircleCollider2D circle)
        {
            circle.radius *= 1.3f;
            originalSize = new Vector2(circle.radius, 0f);
        }
    }
    public void IncreaseDamage()
    {
        damagePerSecond += 1.5f;
    }

    public void ResetStats() //gọi từ script chuyển scene
    {
        damagePerSecond = defaultDamage;
    }


}