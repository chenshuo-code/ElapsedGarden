using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public TimerSys TimerSystem;
    [HideInInspector] public TimeManager TimeManager;
    [HideInInspector] public SoundManager SoundManager;
    [HideInInspector] public UIManager UIManager;
    [HideInInspector] public PlayerController PlayerController;
    [HideInInspector] public GuideFluxBehaviour GuideFlux;
    [HideInInspector] public FirstTreeBehaviour FirstTreeBehaviour;

    [HideInInspector] public  List<PlantBehaviour> ListPlantsActive; //list of active plants from the last check point
    [HideInInspector] public List<CheckPoint> ListCheckPoints; //list of active check point

    private int BGMParameterCount = 0;
    private int BGMParameter = 0;
    private void Awake()
    {
        Instance = this;

        //Init instance managers
        TimerSystem = FindObjectOfType<TimerSys>();
        TimerSystem.InitSys();
        TimeManager = FindObjectOfType<TimeManager>();
        TimeManager.Init();
        SoundManager = FindObjectOfType<SoundManager>();
        SoundManager.Init();
        PlayerController = FindObjectOfType<PlayerController>();
        PlayerController.Init();
        GuideFlux = FindObjectOfType<GuideFluxBehaviour>();
        GuideFlux.Init();

        ListPlantsActive = new List<PlantBehaviour>();
        ListCheckPoints = new List<CheckPoint>();

        SceneManager.LoadSceneAsync("MainLevel1", LoadSceneMode.Additive);

        SoundManager.BackGroundMusicSound.start();
    }
    public void AddPlantActive(PlantBehaviour plant)
    {
        ListPlantsActive.Add(plant);

        BGMParameterCount++;
        if (BGMParameterCount>=10)
        {
            BGMParameter++;
            BGMParameterCount = 0;
        }
        print(BGMParameter);
        SoundManager.BackGroundMusicSound.setParameterByName("Parameter 1", BGMParameter);
    }

    /// <summary>
    /// Call on player is run out of flux
    /// </summary>
    public void GameOver()
    {
        GuideFlux.IsPlayerAlive = false;
    }

    /// <summary>
    /// Call on player arrive the check point
    /// </summary>
    public void CheckGame()
    {
        //UIManager.ShowReloadGameUI();
        GuideFlux.OnRecharge();
    }
    /// <summary>
    /// Call on player active the check point
    /// </summary>
    public void ActivateCheckPoint(CheckPoint checkPoint)
    {
        ListCheckPoints.Add(checkPoint);
        CheckGame();

        int _ParaPhase = (ListCheckPoints.Count - 1);
        if (_ParaPhase>=3)
        {
            _ParaPhase = 3;
        }
        SoundManager.BackGroundMusicSound.setParameterByName("Phase", _ParaPhase);
    }
}
