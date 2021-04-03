using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas ui;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject topUi;

    [SerializeField] Button startButton, optionsButton, exitButton;
    [SerializeField] TextMeshProUGUI scourText;
    [SerializeField] TextMeshProUGUI remainingCountText;
    [SerializeField] TextMeshProUGUI livesText;

    [SerializeField] GameObject countDownScreen;
    [SerializeField] TextMeshProUGUI countDownText;

    #region Unity Calls
    private void OnEnable()
    {
        if (startButton != null)
            startButton.onClick.AddListener(RestartGame);

        if (optionsButton != null)
            optionsButton.onClick.AddListener(Options);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);
    }

    private void Start() => GameManager.Instance.RegisterPoints(UpdateUI);
    #endregion

    public void ShowCountDown(bool show) => countDownScreen.SetActive(show);
    public void ChangeCountDownText(string text) => countDownText.text = text;

    public void ShowPauseScreen(bool showScreen)
    {
        pauseMenu.gameObject.SetActive(showScreen);
    }

    private void UpdateUI(UIData data)
    {
        if (!topUi.activeInHierarchy && topUi != null)
        {
            topUi.SetActive(true);
            ForceAlignment();
        }

        scourText.text = data.points.ToString();
        remainingCountText.text = $"{data.totalAstroidsToWin} / {data.reminderAstroids}";

        int numberOfLives = data.totalLives;

        livesText.text = numberOfLives.ToString();

        if (numberOfLives <= 0)
            livesText.text = Constants.GAME_OVER_TEXT;
    }

    private void ForceAlignment()
    {
        scourText.alignment = TextAlignmentOptions.Center;
        remainingCountText.alignment = TextAlignmentOptions.Center;
        livesText.alignment = TextAlignmentOptions.Center;
    }
    private void RestartGame()
    {
        //GameManager.Instance.RestartLevel();
    }

    private void Options() { }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
