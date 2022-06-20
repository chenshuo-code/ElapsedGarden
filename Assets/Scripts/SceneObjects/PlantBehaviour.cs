using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantBehaviour : MonoBehaviour
{
    public float MaxLifeFlux; //Max flux cost of this plant
    public float LifeAccumulateSpeed; //Speed of life accumulation
    public float LifeDeductTime; //Time deducte with game time passed
    public bool ActiveDeductByTime;//If active, plant's life will deduct with time
    public bool IsAlive; //If this plant is activate in alive

    public Mesh meshFinal; //final mesh on plant active

    private float lifeFlux;//Current life flux


    private bool canActivate; //boolean to active plant
    private bool canDeactivate;

    private bool isInitFinish=false;

    //Components
    private Color aliveColor; //Actual color when plant alive
    private Material material;
    private MeshFilter meshFilter;
    private Mesh meshBase;

    //Script class
    private GuideFluxBehaviour guideFlux; //Get GuideFlux
    private PlayerController playerController; //Get player controller
    private TimeManager timeManager;//get time manager

    //UI
    private Transform canvas;
    private Image lifeBar;
    private float lifeDisplayRate;
    private TMP_Text LifeNum;


    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        IsAlive = false;
        canActivate = false;
        canDeactivate = false;

        guideFlux = GameManager.Instance.GuideFlux;
        playerController = GameManager.Instance.PlayerController;
        timeManager = GameManager.Instance.TimeManager;
        timeManager.EventTimePass += DeductLifeWithGameTime;


        lifeFlux = 0;

        material = transform.GetComponent<Renderer>().material;
        aliveColor = material.color;
        material.color = Color.grey;
        meshFilter = transform.GetComponent<MeshFilter>();
        meshBase = meshFilter.mesh;

        canvas = transform.Find("Canvas");
        lifeBar = transform.Find("Canvas/LifeBar").GetComponent<Image>();
        LifeNum = transform.Find("Canvas/LifeNum").GetComponent<TMP_Text>();
        lifeDisplayRate = 1 / MaxLifeFlux;

        isInitFinish = true;

    }
    private void Update()
    {
        if (isInitFinish)
        {
            //show UI life bar
            if (canvas != null)
            {
                lifeBar.fillAmount = lifeFlux * lifeDisplayRate;
                LifeNum.text = lifeFlux.ToString();
            }
            if (!IsAlive)
            {
                //Activating plant
                if (canActivate)
                {
                    lifeFlux += LifeAccumulateSpeed;

                    if (lifeFlux >= MaxLifeFlux)
                    {
                        ActivatePlant();
                        guideFlux.ReduceFlux(MaxLifeFlux); // Reduce flux in FirstTree
                    }
                    else if (lifeFlux >= guideFlux.CurrentFlux)
                    {
                        //guideFlux.ReduceFlux(guideFlux.CurrentFlux);
                        GameManager.Instance.GameOver();
                    }
                    //If player didn't have enough flux, we take all player's flux

                }
                else if (lifeFlux > 0)
                {
                    lifeFlux -= LifeAccumulateSpeed;
                }
            }
            else //Recover flux //Not in use
            {
                if (canDeactivate)
                {
                    lifeFlux -= LifeAccumulateSpeed;

                    if (lifeFlux <= 0)
                    {
                        ReturnFlux();
                        print(isInitFinish);
                    }
                }
            }
        }
       

    }
    #region Interaction player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && guideFlux.IsPlayerAlive)
        {
            canActivate = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
    #endregion

    /// <summary>
    /// Desactivate plant and add flux to first tree (not in use)
    /// </summary>
    private void ReturnFlux()
    {
        lifeFlux = 0;
        guideFlux.AddFlux(MaxLifeFlux);
        canDeactivate = false;
        DeactivatePlant();
    }

    /// <summary>
    /// Function to deduct plant's life with time pass (not in use)
    /// </summary>
    private void DeductLifeWithGameTime()
    {
        if (IsAlive && ActiveDeductByTime)
        {
            lifeFlux -= LifeDeductTime;
            if (lifeFlux <= 0)
            {
                DeactivatePlant();
            }
        }
    }


    #region Functions public
    public virtual void ActivatePlant()
    {
        GameManager.Instance.AddPlantActive(this); //To be test

        IsAlive = true;
        canActivate = false; //stop cumulate activate rate
        material.color = aliveColor; //Active Color 
        meshFilter.mesh = meshFinal;
        lifeFlux = MaxLifeFlux;
    }
    public virtual void DeactivatePlant()
    {
        IsAlive = false;
        material.color = Color.gray;
        lifeFlux = 0;
    }
    #endregion



}
