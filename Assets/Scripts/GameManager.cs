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

    private void Awake()
    {
        Instance = this;

        //Init instance managers
        TimeManager = FindObjectOfType<TimeManager>();
        TimeManager.Init();
        UIManager = FindObjectOfType<UIManager>();
        UIManager.Init();
        PlayerController = FindObjectOfType<PlayerController>();
        PlayerController.Init();
        GuideFlux = FindObjectOfType<GuideFluxBehaviour>();
        GuideFlux.Init();
        FirstTreeBehaviour = FindObjectOfType<FirstTreeBehaviour>();
        FirstTreeBehaviour.Init();


        ListPlantsActive = new List<PlantBehaviour>();
    }
    public void AddPlantActive(PlantBehaviour plant)
    {
        ListPlantsActive.Add(plant);
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
        PlayerController.EffaceLine();
        PlayerController.TeleportToPosition(FirstTreeBehaviour.transform.position); // Have to change to the last check point
        GuideFlux.RefreshFlux();
    }

    /// <summary>
    /// Call on player arrive the check point
    /// </summary>
    public void CheckGame()
    {
        ListPlantsActive.Clear();
        PlayerController.EffaceLine();
        GuideFlux.RefreshFlux();
    }
}
