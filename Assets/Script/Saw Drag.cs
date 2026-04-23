using UnityEngine;

public class SawDrag : MonoBehaviour
{
    private Camera cam;
    private bool isDragging;

    private Vector3 offset;
    private SawSlot currentSlot;
    private bool isSnapped = false;

    [Header("Settings")]
    public float snapDistance = 1f;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        if (isSnapped) return;
        isDragging = true;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mouseWorld.x, mouseWorld.y, 0);
    }

    private void OnMouseDrag()
    {
        if (!isDragging || isSnapped) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorld.x, mouseWorld.y, 0) + offset;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        TrySnap();
    }

    void TrySnap()
    {
        SawSlot[] slots = FindObjectsOfType<SawSlot>();

        SawSlot closest = null;
        float minDist = snapDistance;

        foreach (var slot in slots)
        {
            if (slot.isOccupied) continue;

            float dist = Vector2.Distance(transform.position, slot.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = slot;
            }
        }

        if (closest != null)
        {
            SnapToSlot(closest);
        }
    }

    void SnapToSlot(SawSlot slot)
    {
        transform.position = slot.transform.position;

        currentSlot = slot;
        slot.isOccupied = true;
        isSnapped = true;
    }
}