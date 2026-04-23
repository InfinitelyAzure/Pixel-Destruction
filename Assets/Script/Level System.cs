using UnityEngine;
using System;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance;

    [Header("Level Settings")]
    public int currentLevel = 1;
    public int currentEXP = 0;
    [HideInInspector] public int expToNextLevel = 200;

    [Header("Scaling")]
    public float expGrowth = 1.5f;
    public static event Action OnEXPChanged;
    public static event Action OnLevelUp;

    private void Awake()
    {
        Instance = this;
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;

        CheckLevelUp();

        OnEXPChanged?.Invoke(); //Gọi UI
    }

    void CheckLevelUp()
    {
        while (currentEXP >= expToNextLevel)
        {
            currentEXP -= expToNextLevel;
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentLevel++;

        expToNextLevel = Mathf.RoundToInt(expToNextLevel * expGrowth);

        OnLevelUp?.Invoke(); //Gọi UI

        CardSelectionUI.Instance.ShowCards();
    }
}