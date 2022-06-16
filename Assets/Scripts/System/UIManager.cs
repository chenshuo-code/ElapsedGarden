using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private Transform inGameCanvas;
    private Transform reloadGameCanvas;

    private TMP_Text gameTimeCount;

    private TimeManager timeManager;
    private PlayerController playerController;

    private int indexCheckPoint = 0;
    public void Init()
    {
        timeManager = GameManager.Instance.TimeManager;
        playerController = GameManager.Instance.PlayerController;

        inGameCanvas = transform.Find("InGameCanvas");
        gameTimeCount = inGameCanvas.Find("GameTimeCount").GetComponent<TMP_Text>();

        reloadGameCanvas = transform.Find("ReloadGameCanvas");
        reloadGameCanvas.gameObject.SetActive(false);

        timeManager.EventTimePass += ShowGameTime;
        ShowGameTime();
    }

    private void ShowGameTime()
    {
        gameTimeCount.text = timeManager.GameTime.ToString();
    }

    #region public, On game reload
    public void ShowReloadGameUI()
    {
        reloadGameCanvas.gameObject.SetActive(true);
    }
    public void HideReloadGameUI()
    {
        reloadGameCanvas.gameObject.SetActive(false);
    }
    public void TurnToLeftCheckPoint()
    {
        indexCheckPoint--;
        indexCheckPoint = Mathf.Clamp(indexCheckPoint, 0,GameManager.Instance.ListCheckPoints.Count-1);
        playerController.TeleportToPosition(GameManager.Instance.ListCheckPoints[indexCheckPoint].transform.position);
    }
    public void TurnToRightCheckPoint()
    {
        indexCheckPoint++;
        indexCheckPoint = Mathf.Clamp(indexCheckPoint, 0, GameManager.Instance.ListCheckPoints.Count-1);
        playerController.TeleportToPosition(GameManager.Instance.ListCheckPoints[indexCheckPoint].transform.position);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
