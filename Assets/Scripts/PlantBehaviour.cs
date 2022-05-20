using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantBehaviour : MonoBehaviour
{
    public float MaxLifeTime; //Max time cost of this plant
    public Color AliveColor;

    private float lifeTime;
    private bool isAlive; //If this plant is activate in alive
    private Material material;
    private PlayerController playerController; //Get player controller
    private FirstTreeBehaviour firstTree; //Get first tree
    private TimeManager timeManager;//get time manager

    //UI
    private Image lifeBar;
    private float lifeDisplayRate;

    private void Start()
    {
        isAlive = false;
       
        playerController = GameManager.Instance.PlayerController;
        firstTree = GameManager.Instance.FirstTreeBehaviour;
        timeManager = GameManager.Instance.TimeManager;

        lifeTime = 0;

        material = transform.GetComponent<Renderer>().material;
        material.color = Color.grey;

        lifeBar = transform.Find("Canvas/LifeBar").GetComponent<Image>();
        lifeDisplayRate = 1 / MaxLifeTime;

    }
    private void Update()
    {
        lifeBar.fillAmount = lifeTime * lifeDisplayRate;
    }

    private void OnMouseEnter()
    {
        if (!isAlive && playerController.PlayerState==PlayerState.OnSpend)
        {
            ActivatePlant();
        }
    }

    private void ActivatePlant()
    {
        playerController.DrawLine(false); // Cancel draw line
        firstTree.GivingTime(MaxLifeTime); // Minus time in FirstTree
        isAlive = true;
        material.color = AliveColor;
        GameManager.Instance.CountPlantActivate(1);
        timeManager.OnCyclePassed();
        
    }
    private void OnMouseDown()
    {
        if (isAlive)
        {
            ReturnTime();
        }
    }

    private void ReturnTime()
    {
        playerController.PlayerState = PlayerState.OnReturn;
        playerController.DrawLine(true);
        playerController.TimeShipping += MaxLifeTime;
        playerController.Plant = this;
    }

    public void DeactivatePlant()
    {
        isAlive = false;
        material.color = Color.gray;
        GameManager.Instance.CountPlantActivate(-1);
    }

}
