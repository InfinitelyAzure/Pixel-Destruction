using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [HideInInspector] public Vector2Int gridPos; //Được sử dụng bởi SpriteToGrid
    [SerializeField] float maxHP = 10f;
    float currentHP;
    private List<Chunk> neighbors = new List<Chunk>();
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0f)
        {
            Break();
        }
    }

    public void AddNeighbor(Chunk other)
    {
        if (neighbors.Contains(other)) return;

        neighbors.Add(other);

        // 🔥 Create rigid connection
        var joint = gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = other.GetComponent<Rigidbody2D>();
        joint.enableCollision = false;
       
    }
    public void Break()
    {
        var joints = GetComponents<FixedJoint2D>();

        foreach (var j in joints)
        {
            if (j.connectedBody != null)
            {
                var other = j.connectedBody.GetComponent<Chunk>();
                if (other != null)
                    other.RemoveNeighbor(this);
            }
            Destroy(j);
        }
        neighbors.Clear();
    }

    public void RemoveNeighbor(Chunk other)
    {
        neighbors.Remove(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Chunk chunk = other.GetComponent<Chunk>();
        if (chunk == null) return;
        var rbChunk = chunk.GetComponent<Rigidbody2D>();
        if (rbChunk != null)
        { //Force có vẻ ngon hơn Impulse
            rbChunk.AddForce((rbChunk.position - rb.position).normalized * 2f, ForceMode2D.Force);
        }
    }
}