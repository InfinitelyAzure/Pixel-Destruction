using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour
{
    public Slider expSlider;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI levelText;

    private void OnEnable()
    {
        LevelSystem.OnEXPChanged += UpdateUI;
        LevelSystem.OnLevelUp += UpdateUI;
    }

    private void OnDisable()
    {
        LevelSystem.OnEXPChanged -= UpdateUI;
        LevelSystem.OnLevelUp -= UpdateUI;
    }

    private void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        var ls = LevelSystem.Instance;

        expSlider.maxValue = ls.expToNextLevel;
        expSlider.value = ls.currentEXP;

        expText.text = $"{ls.currentEXP} / {ls.expToNextLevel}";

        levelText.text = $"Level {ls.currentLevel}";
    }

}