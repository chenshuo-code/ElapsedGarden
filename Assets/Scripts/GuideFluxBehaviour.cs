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

    private TimeManager timeManager;

    private float totalFlux; //Flux of player

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

        totalFlux = MaxFlux;

        timeManager.EventTimePass += OnTimePassed;
    }

    private void Update()
    {
        //ShowUI
        lifeBar.fillAmount = totalFlux * lifeDisplayRate;
        LifeNum.text = totalFlux.ToString();

    }
    private void OnTimePassed()
    {
        if (ActiveDeductByTime) totalFlux -= LifeDeductByTime;
    }

    #region Public, Control flux

    /// <summary>
    /// Reduce flux of first tree
    /// </summary>
    /// <param name="fluxGiven">Flux to spend</param>
    public void ReduceFlux(float fluxGiven)
    {
        totalFlux -= fluxGiven;
        if (totalFlux <= 0)
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
        totalFlux += fluxReceive;
        if (totalFlux >= MaxFlux)
        {
            totalFlux = MaxFlux;
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

    public void RefreshFlux()
    {
        totalFlux = MaxFlux;
    }
    #endregion
}
