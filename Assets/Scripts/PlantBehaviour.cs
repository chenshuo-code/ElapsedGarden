using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantBehaviour : MonoBehaviour
{
    public float MaxLifeTime; //Max time cost of this plant
    public float LifeDeductTime; //Time deducte with game time passed
    public float LifeAccumulateSpeed; //Speed accumulate life time
    public Color AliveColor;

    private float lifeTime; 
    private bool isAlive; //If this plant is activate in alive
    private bool isLifeMax;

    private Material material;
    private PlayerController playerController; //Get player controller
    private FirstTreeBehaviour firstTree; //Get first tree
    private TimeManager timeManager;//get time manager

    //UI
    private Transform canvas;
    private Image lifeBar;
    private float lifeDisplayRate;
    private TMP_Text LifeNum;
    
    private void Start()
    {
        isAlive = false;
        isLifeMax = false;
       
        playerController = GameManager.Instance.PlayerController;
        firstTree = GameManager.Instance.FirstTreeBehaviour;
        timeManager = GameManager.Instance.TimeManager;
        timeManager.EventTimePass += DeductLifeWithGameTime;
        

        lifeTime = 0;

        material = transform.GetComponent<Renderer>().material;
        material.color = Color.grey;

        canvas = transform.Find("Canvas");
        lifeBar = transform.Find("Canvas/LifeBar").GetComponent<Image>();
        LifeNum = transform.Find("Canvas/LifeNum").GetComponent<TMP_Text>();
        canvas.gameObject.SetActive(false);
        lifeDisplayRate = 1 / MaxLifeTime;

    }
    private void Update()
    {
        if (isAlive) 
        {
            lifeBar.fillAmount = lifeTime * lifeDisplayRate;
            LifeNum.text =  lifeTime.ToString();
        } 
    }
    #region Interaction player
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
        material.color = AliveColor; //Active Color Plant
        canvas.gameObject.SetActive(true); //Show life bar

        GameManager.Instance.CountPlantActivate(1); 
        timeManager.OnCyclePassed(); //cycle count
        
    }

    private void OnMouseOver()
    {
        if (isAlive&&!isLifeMax&&playerController.PlayerState==PlayerState.OnSpend)
        {

            lifeTime += Time.deltaTime * LifeAccumulateSpeed;

            if (lifeTime >= MaxLifeTime) isLifeMax = true;
        }
    }

    private void OnMouseExit()
    {
        
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
        playerController.TimeShipping += lifeTime;
        playerController.Plant = this;
    }
    #endregion

    private void DeductLifeWithGameTime()
    {
        if (isAlive)
        {
            lifeTime -= LifeDeductTime;
            if (lifeTime < 0)
            {
                DeactivatePlant();
            }
        }
    }

    #region Functions public
    public void DeactivatePlant()
    {
        isAlive = false;
        material.color = Color.gray;
        lifeTime = 0;
        GameManager.Instance.CountPlantActivate(-1);
        canvas.gameObject.SetActive(false); //disable life bar
    }
    #endregion

}
