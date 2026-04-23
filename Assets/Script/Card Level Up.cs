using UnityEngine;
using System.Collections.Generic;

public class CardSelectionUI : MonoBehaviour
{
    public static CardSelectionUI Instance;

    [Header("UI")]
    public GameObject panel;

    [Header("Slots")]
    public Transform[] slots; 

    [Header("Card Pool (Prefabs)")]
    public List<GameObject> cardPoolPrefab;

    private List<GameObject> spawnedCards = new List<GameObject>();

    private bool hasSelected = false;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowCards()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;

        hasSelected = false;

        ClearOldCards();
        SpawnRandomCards();
    }

    void SpawnRandomCards()
    {
        List<GameObject> poolCopy = new List<GameObject>(cardPoolPrefab);

        for (int i = 0; i < slots.Length; i++)
        {
            if (poolCopy.Count == 0) break;

            int index = Random.Range(0, poolCopy.Count);
            GameObject prefab = poolCopy[index];
            poolCopy.RemoveAt(index);

            GameObject card = Instantiate(prefab, slots[i]);

            RectTransform rt = card.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;
            rt.anchoredPosition = Vector2.zero;

            card.GetComponent<CardButton>()?.Setup();
            spawnedCards.Add(card);
        }
    }

    void ClearOldCards()
    {
        foreach (var card in spawnedCards)
        {
            if (card != null)
                Destroy(card);
        }

        spawnedCards.Clear();
    }

    public void OnButtonClicked()
    {
        if (hasSelected) return;

        hasSelected = true;


        ClosePanel();
    }

    void ClosePanel()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}