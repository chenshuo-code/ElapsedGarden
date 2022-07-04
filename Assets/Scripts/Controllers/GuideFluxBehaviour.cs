using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class to control player's total flux
/// </summary>
public class GuideFluxBehaviour : MonoBehaviour
{
    public float MaxFlux; 
    public float LifeDeductByTime; //Life deduct when time passed
    /// <summary>
    /// Material of guide flux when it is dead
    /// </summary>
    public Material MatDead;

    public bool ActiveDeductByTime;

    [HideInInspector]public bool IsPlayerAlive; //Detect if player is in state alive
    [HideInInspector] public float CurrentFlux; //Flux of player

    private TimeManager timeManager;

    //FeedBack visual
    private Material MatInit;
    private MeshRenderer meshRenderer;

    //Trail
    private TrailRenderer trail;
    private float initTrailWidth;

    //Particle System
    private ParticleSystem particleTrace;
    private ParticleSystem particleTrail;

    private float initPSTrailEmissionRate;

    //UI
    private Transform canvas;
    private Image lifeBar;
    private float lifeDisplayRate;
    private TMP_Text LifeNum;

    public void Init()
    {
        timeManager = GameManager.Instance.TimeManager;

        canvas = transform.Find("Canvas");
        lifeBar = transform.Find("Canvas/LifeBar").GetComponent<Image>();
        LifeNum = transform.Find("Canvas/LifeNum").GetComponent<TMP_Text>();

        particleTrace = transform.Find("PSTrace").GetComponent<ParticleSystem>();
        particleTrail = transform.Find("PSTrail").GetComponent<ParticleSystem>();

        trail = GetComponentInChildren<TrailRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
        MatInit = meshRenderer.material;

        lifeDisplayRate = 1 / MaxFlux;

        CurrentFlux = MaxFlux;

        initTrailWidth = trail.endWidth;
        initPSTrailEmissionRate = particleTrail.emissionRate;


        timeManager.EventTimePass += OnTimePassed;

        IsPlayerAlive = true;
    }

    private void Update()
    {
        //FeedBack
        trail.startWidth = initTrailWidth / MaxFlux * CurrentFlux;
        particleTrail.emissionRate = initPSTrailEmissionRate/ MaxFlux * CurrentFlux;


        //ShowUI
        lifeBar.fillAmount = CurrentFlux * lifeDisplayRate;
        LifeNum.text = CurrentFlux.ToString();

    }
    private void OnTimePassed()
    {
        if (ActiveDeductByTime) CurrentFlux -= LifeDeductByTime;
    }

    /// <summary>
    /// When player is run out of flux
    /// </summary>
    private void OnGameOver()
    {
        meshRenderer.material = MatDead;
        print(meshRenderer.material.name) ;
        trail.emitting = false;
        particleTrace.Stop(true);
        particleTrail.Stop(true);
    }


    #region Public, Control flux

    /// <summary>
    /// Reduce flux of first tree
    /// </summary>
    /// <param name="fluxGiven">Flux to spend</param>
    public void ReduceFlux(float fluxGiven)
    {
        
        if (CurrentFlux >= 0)
        {
            CurrentFlux -= fluxGiven;
            
        }
        else
        {
            GameManager.Instance.GameOver();
            OnGameOver();
        }
    }
    /// <summary>
    /// Add flux of first tree
    /// </summary>
    /// <param name="fluxReceive">Flux to receive</param>
    public void AddFlux(float fluxReceive)
    {
        CurrentFlux += fluxReceive;
        if (CurrentFlux >= MaxFlux)
        {
            CurrentFlux = MaxFlux;
        }
    }
    /// <summary>
    /// Increase limit of max flux
    /// </summary>
    /// <param name="fluxIncrease">Flux to increased</param>
    public void IncreaseMaxFlux(float fluxIncrease)
    {
        MaxFlux += fluxIncrease;
    }
    /// <summary>
    /// Call when player is arrived on check point
    /// </summary>
    public void OnRecharge()
    {
        CurrentFlux = MaxFlux;
        meshRenderer.material = MatInit;

        trail.emitting = true;
        trail.startWidth = initTrailWidth;
        particleTrace.Play(true);
        particleTrail.Play(true);
    }
    #endregion
}
