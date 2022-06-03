using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantBehaviour : MonoBehaviour
{
    public float MaxLifeTime; //Max time cost of this plant
    public float LifeDeductTime; //Time deducte with game time passed
    public float LifeAccumulateSpeed;
    public Mesh meshFinal;

    private float lifeTime;

    private float deltaTime; // Delta time cumule during life giving
    private bool isAlive; //If this plant is activate in alive
    private bool isLifeMax;
    private bool canActivate;
    private bool canDeactivate;
    private bool canStartFrom; //Can player continue start from this plant

    //Components
    private Color AliveColor;
    private Material material;
    private MeshFilter meshFilter;
    private Mesh meshBase;

    //Script class
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
        canActivate = false;
        canDeactivate = false;
        canStartFrom = false;


        playerController = GameManager.Instance.PlayerController;
        firstTree = GameManager.Instance.FirstTreeBehaviour;
        timeManager = GameManager.Instance.TimeManager;
        timeManager.EventTimePass += DeductLifeWithGameTime;
        

        lifeTime = 0;

        material = transform.GetComponent<Renderer>().material;
        AliveColor = material.color;
        material.color = Color.grey;
        meshFilter = transform.GetComponent<MeshFilter>();
        meshBase = meshFilter.mesh;

        canvas = transform.Find("Canvas");
        lifeBar = transform.Find("Canvas/LifeBar").GetComponent<Image>();
        LifeNum = transform.Find("Canvas/LifeNum").GetComponent<TMP_Text>();
        lifeDisplayRate = 1 / MaxLifeTime;

    }
    private void Update()
    {
        //show UI life bar
        lifeBar.fillAmount = lifeTime * lifeDisplayRate;
        LifeNum.text = lifeTime.ToString();

        if (!isAlive)
        {
            //Activating plant
            if (canActivate)
            {
                lifeTime += LifeAccumulateSpeed;

                if (lifeTime >= MaxLifeTime)
                {
                    lifeTime = MaxLifeTime;
                    ActivatePlant();
                }

            }
            else if (lifeTime > 0)
            {
                lifeTime -= LifeAccumulateSpeed;
            }
        }else
        {
            if (canDeactivate)
            {
                lifeTime -= LifeAccumulateSpeed;

                if (lifeTime <= 0)
                {
                    lifeTime = 0;
                    ReturnTime();
                }
            }
        }

    }
    #region Interaction player
    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isAlive && playerController.PlayerState == PlayerState.OnSpend)
            {
                canActivate = true;
            }
            else
            {
                if (playerController.PlayerState == PlayerState.OnSpend)
                {
                    canStartFrom = true;
                }
            }
        }
        else if (Input.GetMouseButton(1))
        {
            if (isAlive)
            {
                canDeactivate = true;
            }
        }
    }

    private void OnMouseOver()
    {
        if (isAlive && canStartFrom)
        {
            if (Input.GetMouseButton(0))
            {
                SpendingTime();
                print("spending");
            }
            if (Input.GetMouseButton(1))
            {
                ReturningTime();
            }
        }
    }

    private  void OnMouseExit()
    {
        canActivate = false;
        canDeactivate = false;
    }
    private void ActivatePlant()
    {
        firstTree.GivingTime(MaxLifeTime); // Minus time in FirstTree
        isAlive = true;
        canActivate = false; //stop cumulate activate rate
        material.color = AliveColor; //Active Color 
        meshFilter.mesh = meshFinal;

        canStartFrom = true;
        GameManager.Instance.CountPlantActivate(1); 
        timeManager.OnCyclePassed(); //cycle count
        
    }

    private void ReturnTime()
    {
        firstTree.ReceiveTime(MaxLifeTime);
        canDeactivate = false;
        DeactivatePlant();
    }
    #endregion

    private void DeductLifeWithGameTime()
    {
        if (isAlive)
        {
            lifeTime -= LifeDeductTime;
            if (lifeTime <= 0)
            {
                DeactivatePlant();
            }
        }
    }
    private void SpendingTime()
    {
        playerController.PlayerState = PlayerState.OnSpend;
        playerController.ActivateDrawLine(true);
    }
    private void ReturningTime()
    {
        canDeactivate = true;
        playerController.PlayerState = PlayerState.OnReturn;
        playerController.ActivateDrawLine(true);
    }

    #region Functions public


    public void DeactivatePlant()
    {
        isAlive = false;
        material.color = Color.gray;
        canStartFrom = false;
        lifeTime = 0;
        isLifeMax = false;
        GameManager.Instance.CountPlantActivate(-1);
    }
    public void DeactivateStartFrom()
    {
        canStartFrom = false;
    }
    #endregion



}
