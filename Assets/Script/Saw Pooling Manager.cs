using UnityEngine;

public class SawPoolingManager : MonoBehaviour
{
    [Header("Pooling")]
    public Saw[] sawPoolPrefab;
    public void AddSaw()
    {
        foreach (var saw in sawPoolPrefab)
        {
            if (!saw.gameObject.activeInHierarchy)
            {
                saw.gameObject.SetActive(true);
                return;
            }
        }
    }
}
