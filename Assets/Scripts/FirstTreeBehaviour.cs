using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstTreeBehaviour : MonoBehaviour
{
    public float TotalLifeTime; //Total life time for player
    public float LifeDeductCycle; //Time deduct in each cycle
    public float LifeDeductTime; //Life deduct when time passed

    public bool ActiveDeductCycle;
    public bool ActiveDeductTime;

    private PlayerController playerController; //Get player controller
    private TimeManager timeManager;


    //UI
    private Image lifeBar;
    private float lifeDisplayRate;
    public void Init()
    {
        playerController = GameManager.Instance.PlayerController;
        timeManager = GameManager.Instance.TimeManager;

        TotalLifeTime = 100;

        lifeBar = transform.Find("Canvas/LifeBar").GetComponent<Image>();
        lifeDisplayRate = 1 / TotalLifeTime;

        timeManager.EventNewCycle += OnCyclePassed;
        timeManager.EventTimePass += OnTimePassed;
        
    }

    private void Update()
    {
        lifeBar.fillAmount = TotalLifeTime * lifeDisplayRate;
        if (TotalLifeTime <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
    private void OnMouseDown()
    {
        SpendTime();
    }
    private void OnMouseEnter()
    {
        ReturnTime();
    }

    private void SpendTime()
    {
        playerController.PlayerState = PlayerState.OnSpend;
        playerController.DrawLine(true);
    }
    private void ReturnTime()
    {
        if (playerController.PlayerState == PlayerState.OnReturn && playerController.Plant!=null)
        {
            playerController.DrawLine(false);
            playerController.Plant.DeactivatePlant();
            TotalLifeTime += playerController.TimeShipping;
            playerController.TimeShipping = 0;
            playerController.PlayerState = PlayerState.OnStay;

            timeManager.OnCyclePassed();
            print(TotalLifeTime);
        }
    }

    private void OnCyclePassed()
    {
        if (ActiveDeductCycle) TotalLifeTime -= LifeDeductCycle;
    }
    private void OnTimePassed()
    {
        if (ActiveDeductTime) TotalLifeTime -= LifeDeductTime;
    }
    public void GivingTime(float timeGiven)
    {
        TotalLifeTime -= timeGiven;
    }
    
}
