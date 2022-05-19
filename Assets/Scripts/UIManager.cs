using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    private Transform inGameCanvas;
    private Transform afterGameCanvas;

    private Transform gameWin;
    private Transform gameOver;

    private TMP_Text gameTimeCount;

    private TimeManager timeManager;
    public void Init()
    {
        timeManager = GameManager.Instance.TimeManager;

        inGameCanvas = transform.Find("InGameCanvas");
        gameTimeCount = inGameCanvas.Find("GameTimeCount").GetComponent<TMP_Text>();

        afterGameCanvas = transform.Find("AfterGameCanvas");
        gameWin = afterGameCanvas.Find("GameWin");
        gameOver = afterGameCanvas.Find("GameOver");

        afterGameCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        gameTimeCount.text = Mathf.Ceil(timeManager.GameTime).ToString();
    }

    public void GameOver()
    {
        afterGameCanvas.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(true);
        gameWin.gameObject.SetActive(false);
    }
    public void GameWin()
    {
        afterGameCanvas.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
        gameWin.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
