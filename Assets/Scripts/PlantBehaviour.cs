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

    public Mesh meshFinal; //final mesh on plant active

    private float lifeFlux;//Current life flux

    private bool isAlive; //If this plant is activate in alive
    private bool canActivate; //boolean to active plant
    private bool canDeactivate;

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
        isAlive = false;
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

    }
    private void Update()
    {
        //show UI life bar
        lifeBar.fillAmount = lifeFlux * lifeDisplayRate;
        LifeNum.text = lifeFlux.ToString();

        if (!isAlive)
        {
            //Activating plant
            if (canActivate)
            {
                lifeFlux += LifeAccumulateSpeed;

                if (lifeFlux >= MaxLifeFlux)
                {
                    lifeFlux = MaxLifeFlux;
                    ActivatePlant();
                }

            }
            else if (lifeFlux > 0)
            {
                lifeFlux -= LifeAccumulateSpeed;
            }
        }else
        {
            if (canDeactivate)
            {
                lifeFlux -= LifeAccumulateSpeed;

                if (lifeFlux <= 0)
                {
                    lifeFlux = 0;
                    ReturnTime();
                }
            }
        }

    }
    #region Interaction player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
    private void ActivatePlant()
    {
        GameManager.Instance.AddPlantActive(this); //To be test

        isAlive = true;
        canActivate = false; //stop cumulate activate rate
        material.color = aliveColor; //Active Color 
        meshFilter.mesh = meshFinal;
        guideFlux.ReduceFlux(MaxLifeFlux); // Reduce flux in FirstTree
    }

    /// <summary>
    /// Desactivate plant and add flux to first tree (not in use)
    /// </summary>
    private void ReturnTime()
    {
        guideFlux.AddFlux(MaxLifeFlux);
        canDeactivate = false;
        DeactivatePlant();
    }
    #endregion

    /// <summary>
    /// Function to deduct plant's life with time pass (not in use)
    /// </summary>
    private void DeductLifeWithGameTime()
    {
        if (isAlive && ActiveDeductByTime)
        {
            lifeFlux -= LifeDeductTime;
            if (lifeFlux <= 0)
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
        lifeFlux = 0;
    }
    #endregion



}
