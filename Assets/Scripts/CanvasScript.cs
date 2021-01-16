using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject LevelCompletePanel;
    public GameObject LoadingPanel;
    public GameObject StartPanel;
    public GameObject GameOverPanel;
    public Slider LoadingSlider;
    public Image DisplayStars;
    public Sprite[] StarImages;
    public Text timerText;
    public int timer;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("LevelComplete"))
            PlayerPrefs.SetInt("LevelComplete", 0);

        StartCoroutine(StartAnimStop());
        timerText.text = timer.ToString();
        StartCoroutine(CountDown());
    }

    private void Update()
    {
        timerText.text = timer.ToString();
    }
    public void OnPlayButton()
    {
        StartCoroutine(LoadAsync(PlayerPrefs.GetInt("LevelComplete") + 1));
    }
    public void OnPauseButton()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }
    public void OnRestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnResumeButton()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

    public void OnMainMenu()
    {
        StartCoroutine(LoadAsync(0));
    }

    public void OnLevelComplete()
    {
        DisplayStars.sprite = StarImages[PlayerPrefs.GetInt("DisplayStars")];
        PlayerPrefs.SetInt("LevelComplete", PlayerPrefs.GetInt("LevelComplete") + 1);
    }
    public void OnNextLevelButton()
    {
        StartCoroutine(LoadAsync(PlayerPrefs.GetInt("LevelComplete") + 1));
    }
    IEnumerator LoadAsync(int sceneBuildIndex)
    {
        LoadingPanel.SetActive(true);
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneBuildIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingSlider.value = 100-progress;
            yield return null;
        }
    }

    IEnumerator StartAnimStop()
    {
        yield return new WaitForSeconds(1f);
        StartPanel.SetActive(false);
    }

    IEnumerator CountDown()
    {
        while (timer > 0)
        {
            timerText.text = timer.ToString();
            yield return new WaitForSeconds(1f);
            timer--;
            if (timer <= 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt("timer", timer);
        }
    }
}
