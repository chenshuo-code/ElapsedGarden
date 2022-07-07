using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantBehaviour : MonoBehaviour
{
    public float MaxLifeFlux; //Max flux cost of this plant
    public float LifeDeductTime; //Time deducte with game time passed
    public bool ActiveDeductByTime;//If active, plant's life will deduct with time
    public bool IsAlive; //If this plant is activate in alive

    private float lifeFlux;//Current life flux

    private bool canActivate; //boolean to active plant
    private bool canDeactivate;

    private bool isInitFinish=false;
    private float lifeDisplayRate;
    private float initPSRingStartSize;

    //Components
    private Color aliveColor; //Actual color when plant alive
    private Material material;
    private SkinnedMeshRenderer skinnedMesh;
    private ParticleSystem particleRing;

    //Script class
    private GuideFluxBehaviour guideFlux; //Get GuideFlux
    private PlayerController playerController; //Get player controller
    private TimeManager timeManager;//get time manager



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

        skinnedMesh = transform.GetComponent<SkinnedMeshRenderer>();

        particleRing = transform.Find("PSRing").GetComponent<ParticleSystem>();
        initPSRingStartSize = particleRing.startSize;

        lifeDisplayRate = 100 / MaxLifeFlux;
    }
    private void Update()
    {
        //Apply mesh changement
        skinnedMesh.SetBlendShapeWeight(0,lifeFlux*lifeDisplayRate);
        particleRing.startSize = initPSRingStartSize - initPSRingStartSize*(lifeFlux * lifeDisplayRate)/100;

        if (!IsAlive)
        {
            //Activating plant
            if (canActivate)
            {

                lifeFlux += guideFlux.ResolveSpeed;
                if (lifeFlux >= MaxLifeFlux)
                {
                    ActivatePlant(false);
                    guideFlux.ReduceFlux(MaxLifeFlux); // Reduce flux in FirstTree
                }
                else if (lifeFlux >= guideFlux.CurrentFlux) //If player didn't have enough flux
                {
                    canActivate = false;
                }

            }
            else if (lifeFlux > 0)
            {
                lifeFlux -= guideFlux.ResolveSpeed;
            }
        }
        else //Recover flux //Not in use
        {
            if (canDeactivate)
            {
                lifeFlux -= guideFlux.ResolveSpeed;

                if (lifeFlux <= 0)
                {
                    ReturnFlux();
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
    /// <summary>
    /// Function to activate plant
    /// </summary>
    /// <param name="needGrow">If we want to see the growth process of plant</param>
    public virtual void ActivatePlant(bool needGrow)
    {
        GameManager.Instance.AddPlantActive(this); //To be test

        IsAlive = true;
        canActivate = false; //stop cumulate activate rate
        material.color = aliveColor; //Active Color 
        gameObject.layer = LayerMask.NameToLayer("Color");

        if (needGrow)
        {
            lifeFlux = Mathf.Lerp(0, MaxLifeFlux,2f) ;
        }
        else
        {
            lifeFlux = MaxLifeFlux;
        }

    }
    public virtual void DeactivatePlant()
    {
        IsAlive = false;
        material.color = Color.gray;
        lifeFlux = 0;
    }
    #endregion



}
