using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public int levelToAdvance;
    public GameObject winPanel;
    private bool isLoading = false;

    [Header("Stage List UI")]
    public GameObject stageListPanel;

    private void OnEnable()
    {
        LevelSystem.OnLevelUp += CheckStageProgress;
    }

    private void OnDisable()
    {
        LevelSystem.OnLevelUp -= CheckStageProgress;
    }

    void CheckStageProgress()
    {
        if (isLoading) return;

        int currentLevel = LevelSystem.Instance.currentLevel;

        if (currentLevel < levelToAdvance) return;

        isLoading = true;
        
        ShowWinUI();
    }

    void ShowWinUI()
    {

        if (winPanel != null)
            winPanel.SetActive(true);
    }


    public void LoadNextScene()
    {
        ResetAllStats();

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            return;
        }

        SceneManager.LoadScene(nextIndex);
        Time.timeScale = 1f;
    }

    void ResetAllStats()
    {
        LevelSystem.Instance.currentLevel = 1;
        LevelSystem.Instance.currentEXP = 0;
        LevelSystem.Instance.expToNextLevel = 200;

        TapToDestroy tap = FindFirstObjectByType<TapToDestroy>();
        if (tap != null)
            tap.ResetStats();

        Saw saw = FindFirstObjectByType<Saw>();
        if (saw != null)
            saw.ResetStats();
    }

    public void ToggleStageList()
    {
        if (stageListPanel == null) return;

        bool isActive = stageListPanel.activeSelf;
        stageListPanel.SetActive(!isActive);

        Time.timeScale = stageListPanel.activeSelf ? 0f : 1f;
    }

    void LoadStageByIndex(int index)
    {
        ResetAllStats();

        if (index >= SceneManager.sceneCountInBuildSettings)
            return;

        SceneManager.LoadScene(index);
        Time.timeScale = 1f;
    }

    public void LoadStage(int index)
    {
        LoadStageByIndex(index);
    }
}