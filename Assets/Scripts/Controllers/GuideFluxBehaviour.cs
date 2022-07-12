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

    public bool ActiveDeductByTime;

    /// <summary>
    /// Speed to resolve plante with flux
    /// </summary>
    public float ResolveSpeed;

    [HideInInspector]public bool IsPlayerAlive; //Detect if player is in state alive
    [HideInInspector] public float CurrentFlux; //Flux of player

    private float tempFlux; // Temporary flux lost 

    private TimeManager timeManager;

    //FeedBack visual
    private MeshRenderer meshRenderer;


    //Particle System
    private ParticleSystem particleTrail;
    private ParticleSystem particleFlux;

    private float initPSTrailEmissionRate;
    private float initPSFluxSize;
    private Color initPSFluxStartColor;


    //UI
    private Transform canvas;
    private Image lifeBar;
    private float lifeDisplayRate;
    private TMP_Text LifeNum;

    public void Init()
    {
        timeManager = GameManager.Instance.TimeManager;

        particleTrail = transform.Find("PSTrail").GetComponent<ParticleSystem>();
        particleFlux = transform.Find("PSFlux").GetComponent<ParticleSystem>();

        //trail = GetComponentInChildren<TrailRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();

        lifeDisplayRate = 1 / MaxFlux;

        CurrentFlux = MaxFlux;

        //initTrailWidth = trail.endWidth;
        initPSTrailEmissionRate = particleTrail.emissionRate;
        initPSFluxSize = particleFlux.startSize;
        initPSFluxStartColor = particleFlux.startColor;

        timeManager.EventTimePass += OnTimePassed;

        IsPlayerAlive = true;

    
    }




    private void Update()
    {
        //FeedBack
        particleTrail.emissionRate = initPSTrailEmissionRate/ MaxFlux * CurrentFlux;
        particleFlux.startSize = initPSFluxSize / MaxFlux * CurrentFlux;

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
        //trail.emitting = false;
        particleTrail.Stop(true);
    }


    #region Public, Control flux

    /// <summary>
    /// Reduce flux of player
    /// </summary>
    /// <param name="fluxGiven">Flux to spend</param>
    /// <param name="isTemporary">is flux spend temporary(can be recharge in check point)</param>
    public void ReduceFlux(float fluxGiven, bool isTemporary)
    {
        
        if (CurrentFlux >= 0)
        {
            if (isTemporary)
            {
                tempFlux += fluxGiven;
            }

            CurrentFlux -= fluxGiven;

            if (CurrentFlux<=MaxFlux*0.3f) //If current flux is lower than 30% of max flux
            {
                particleFlux.startColor = Color.red;
            }
            
        }
        else
        {
            GameManager.Instance.GameOver();
            OnGameOver();
        }
    }
    /// <summary>
    /// Add flux of player
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
    /// Get how many flux to be recharged in this check point
    /// </summary>
    /// <returns>Flux to recharge</returns>
    public float GetFluxToRecharge()
    {
        float _fluxToCharge;
        return _fluxToCharge = MaxFlux - tempFlux - CurrentFlux;
    }

    /// <summary>
    /// Call when player is arrived on check point
    /// </summary>
    /// <returns>Flux to recharge</returns>
    public void OnRecharge()
    {
        CurrentFlux = MaxFlux;
        particleFlux.startColor = initPSFluxStartColor;
        particleTrail.Play(true);

        if (tempFlux > 0) tempFlux = 0;
    }
    #endregion
}
