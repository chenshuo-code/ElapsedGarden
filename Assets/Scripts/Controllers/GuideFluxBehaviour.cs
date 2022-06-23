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

    [HideInInspector]public bool IsPlayerAlive; //Detect if player is in state alive
    [HideInInspector] public float CurrentFlux; //Flux of player

    private TimeManager timeManager;

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

        lifeDisplayRate = 1 / MaxFlux;

        CurrentFlux = MaxFlux;

        timeManager.EventTimePass += OnTimePassed;

        IsPlayerAlive = true;
    }

    private void Update()
    {
        //ShowUI
        lifeBar.fillAmount = CurrentFlux * lifeDisplayRate;
        LifeNum.text = CurrentFlux.ToString();

    }
    private void OnTimePassed()
    {
        if (ActiveDeductByTime) CurrentFlux -= LifeDeductByTime;
    }

    #region Public, Control flux

    /// <summary>
    /// Reduce flux of first tree
    /// </summary>
    /// <param name="fluxGiven">Flux to spend</param>
    public void ReduceFlux(float fluxGiven)
    {
        CurrentFlux -= fluxGiven;
        if (CurrentFlux <= 0)
        {
            GameManager.Instance.GameOver();
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
    /// Reset player's flux
    /// </summary>
    public void ResetFlux()
    {
        CurrentFlux = MaxFlux;
    }
    #endregion
}
