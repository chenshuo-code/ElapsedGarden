using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [HideInInspector] public TimeManager TimeManager;
    [HideInInspector] public UIManager UIManager;
    [HideInInspector] public PlayerController PlayerController;
    [HideInInspector] public GuideFluxBehaviour GuideFlux;
    [HideInInspector] public FirstTreeBehaviour FirstTreeBehaviour;

    [HideInInspector] public  List<PlantBehaviour> ListPlantsActive; //list of active plants from the last check point
    [HideInInspector] public List<CheckPoint> ListCheckPoints; //list of active check point

    private void Awake()
    {
        Instance = this;

        //Init instance managers
        TimeManager = FindObjectOfType<TimeManager>();
        TimeManager.Init();
        PlayerController = FindObjectOfType<PlayerController>();
        PlayerController.Init();
        GuideFlux = FindObjectOfType<GuideFluxBehaviour>();
        GuideFlux.Init();
        UIManager = FindObjectOfType<UIManager>();
        UIManager.Init();
        FirstTreeBehaviour = FindObjectOfType<FirstTreeBehaviour>();
        FirstTreeBehaviour.Init();


        ListPlantsActive = new List<PlantBehaviour>();
        ListCheckPoints = new List<CheckPoint>();
    }
    public void AddPlantActive(PlantBehaviour plant)
    {
        ListPlantsActive.Add(plant);
    }
    public void AddCheckPointActive(CheckPoint checkPoint)
    {
        ListCheckPoints.Add(checkPoint);
    }
    /// <summary>
    /// Call on player is run out of flux
    /// </summary>
    public void GameOver()
    {
        foreach (PlantBehaviour plant in ListPlantsActive)
        {
            plant.DeactivatePlant();
        }
        PlayerController.TeleportToPosition(ListCheckPoints[ListCheckPoints.Count-1].transform.position); // Teleport to the last check point 
        PlayerController.EffaceLine();
        GuideFlux.ResetFlux();
        UIManager.ShowReloadGameUI();
    }

    /// <summary>
    /// Call on player arrive the check point
    /// </summary>
    public void CheckGame()
    {
        UIManager.ShowReloadGameUI();
    }
    /// <summary>
    /// Call on player active the check point
    /// </summary>
    public void ActivateCheckPoint()
    {
        ListPlantsActive.Clear();
        PlayerController.EffaceLine();
        GuideFlux.ResetFlux();
        CheckGame();
    }
}