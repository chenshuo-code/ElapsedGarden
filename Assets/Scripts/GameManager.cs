using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public PlayerController PlayerController;
    [HideInInspector] public FirstTreeBehaviour FirstTreeBehaviour;
    [HideInInspector] public TimeManager TimeManager;
    [HideInInspector] public UIManager UIManager;
    [HideInInspector] public  PlantBehaviour[] Plants;

    private float deactivatePlantsCount;

    private void Awake()
    {
        Instance = this;

        TimeManager = FindObjectOfType<TimeManager>();
        TimeManager.Init();
        UIManager = FindObjectOfType<UIManager>();
        UIManager.Init();
        PlayerController = FindObjectOfType<PlayerController>();
        PlayerController.Init();
        FirstTreeBehaviour = FindObjectOfType<FirstTreeBehaviour>();
        FirstTreeBehaviour.Init();

        Plants = FindObjectsOfType<PlantBehaviour>();
        deactivatePlantsCount = Plants.Length;

    }
    public void CountPlantActivate(float plantCount)
    {
        deactivatePlantsCount -= plantCount;
        if (deactivatePlantsCount <= 0) GameWin();
    }
    public void GameOver()
    {
        UIManager.GameOver();
        Time.timeScale = 0;
    }
    public void GameWin()
    {
        UIManager.GameWin();
        Time.timeScale = 0;
    }
}
