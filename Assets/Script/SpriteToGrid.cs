using System.Collections.Generic;
using UnityEngine;

public class SpriteToGrid : MonoBehaviour
{
    public GameObject chunkPrefab; // optional (can be null)
    public float alphaThreshold = 0.1f;

    private List<Chunk> chunks = new List<Chunk>();

    void Start()
    {
        GenerateChunks();
        ConnectNeighbors();
    }

    void GenerateChunks()
    {
        var sr = GetComponent<SpriteRenderer>();
        var tex = sr.sprite.texture;

        int width = tex.width;
        int height = tex.height;

        float ppu = sr.sprite.pixelsPerUnit;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tex.GetPixel(x, y).a < alphaThreshold)
                    continue;

                Vector2 localPos = new Vector2(
                    (x - sr.sprite.pivot.x) / ppu,
                    (y - sr.sprite.pivot.y) / ppu
                );

                GameObject go = new GameObject($"Chunk_{x}_{y}");
                go.transform.position = transform.TransformPoint(localPos);
                
                //Cho chunk scale với scale của Sprite 
                float pixelWorldSize = 1f / ppu * transform.lossyScale.x;
                go.transform.localScale = Vector3.one * pixelWorldSize;

                var srChunk = go.AddComponent<SpriteRenderer>();
                srChunk.sprite = CreatePixelSprite(tex.GetPixel(x, y));

                var rb = go.AddComponent<Rigidbody2D>();
                rb.gravityScale = 1;

                var col = go.AddComponent<BoxCollider2D>();

                var chunk = go.AddComponent<Chunk>();
                chunk.gridPos = new Vector2Int(x, y);

                chunks.Add(chunk);
            }
        }
        Destroy(gameObject);
    }

    Sprite CreatePixelSprite(Color color)
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, color);
        tex.Apply();

        tex.filterMode = FilterMode.Point;

        return Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
    }

    void ConnectNeighbors()
    {
        Dictionary<Vector2Int, Chunk> map = new Dictionary<Vector2Int, Chunk>();

        foreach (var c in chunks)
            map[c.gridPos] = c;

        foreach (var c in chunks)
        {
            Vector2Int[] dirs = {
                Vector2Int.up,
                Vector2Int.down,
                Vector2Int.left,
                Vector2Int.right
            };

            foreach (var d in dirs)
            {
                if (map.TryGetValue(c.gridPos + d, out Chunk neighbor))
                {
                    c.AddNeighbor(neighbor);
                }
            }
        }
    }
}