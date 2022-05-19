using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstTreeBehaviour : MonoBehaviour
{
    public float TotalLifeTime; //Total life time for player
    public float TimeDeductCycle; //Time deduct in each cycle

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
        playerController.IsSpend = true;
        playerController.DrawLine(true);
    }
    private void ReturnTime()
    {
        if (!playerController.IsSpend && playerController.Plant!=null)
        {
            playerController.DrawLine(false);
            playerController.Plant.DeactivatePlant();
            TotalLifeTime += playerController.TimeShipping;
            playerController.TimeShipping = 0;
            print(TotalLifeTime);
        }
    }

    private void OnCyclePassed()
    {
        TotalLifeTime -= TimeDeductCycle;
    }
    public void GivingTime(float timeGiven)
    {
        TotalLifeTime -= timeGiven;
    }
    
}
