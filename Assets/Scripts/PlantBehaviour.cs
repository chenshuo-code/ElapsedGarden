using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantBehaviour : MonoBehaviour
{
    public float MaxLifeTime; //Max time cost of this plant
    public float LifeDeductTime; //Time deducte with game time passed
    public Color AliveColor;

    private float lifeTime;

    private float deltaTime; // Delta time cumule during life giving
    private bool isAlive; //If this plant is activate in alive
    private bool isLifeMax;
    private bool canActivate;
    private bool canDeactivate;

    private Material material;
    private MeshFilter mesh;
    private Mesh baseState;
    public Mesh finalState;
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



        playerController = GameManager.Instance.PlayerController;
        firstTree = GameManager.Instance.FirstTreeBehaviour;
        timeManager = GameManager.Instance.TimeManager;
        timeManager.EventTimePass += DeductLifeWithGameTime;
        

        lifeTime = 0;

        material = transform.GetComponent<Renderer>().material;
        material.color = Color.grey;
        mesh = transform.GetComponent<MeshFilter>();
        baseState = transform.GetComponent<MeshFilter>().mesh;

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
<<<<<<< Updated upstream
            lifeBar.fillAmount = lifeTime * lifeDisplayRate;
            LifeNum.text = lifeTime.ToString();
        } 
=======
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

>>>>>>> Stashed changes
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
        }
        else if (Input.GetMouseButton(1))
        {
            if (isAlive)
            {
                canDeactivate = true;
            }
        }
    }
<<<<<<< Updated upstream

    private void ActivatePlant()
    {
        playerController.DrawLine(false); // Cancel draw line
        firstTree.GivingTime(MaxLifeTime); // Minus time in FirstTree
        isAlive = true;

        material.color = AliveColor;
        GameManager.Instance.CountPlantActivate(1);
        mesh.mesh = finalState;

       
        //Give lifeTime to Max
        if (canReborn)
        {
            lifeTime = MaxLifeTime;
            canReborn = false;
=======
    private void OnMouseOver()
    {
        if (isAlive)
        {
            if (Input.GetMouseButton(0))
            {
                SpendingTime();
            }
            if (Input.GetMouseButton(1))
            {
                ReturningTime();
            }
>>>>>>> Stashed changes
        }

<<<<<<< Updated upstream

        
        timeManager.OnCyclePassed(); //cycle count
        
    }
    private void OnMouseDown()
=======
    private  void OnMouseExit()
    {
        canActivate = false;
        canDeactivate = false;
    }
    private void ActivatePlant()
>>>>>>> Stashed changes
    {
        firstTree.GivingTime(MaxLifeTime); // Minus time in FirstTree
        isAlive = true;
        canActivate = false; //stop cumulate activate rate
        material.color = AliveColor; //Active Color 

<<<<<<< Updated upstream
    private void ReturnTime()
    {
        playerController.PlayerState = PlayerState.OnReturn;
        playerController.DrawLine(true);
        playerController.TimeShipping += MaxLifeTime;
        playerController.Plant = this;
=======
        GameManager.Instance.CountPlantActivate(1); 
        timeManager.OnCyclePassed(); //cycle count
        
    }

    private void ReturnTime()
    {
        firstTree.ReceiveTime(MaxLifeTime);
        canDeactivate = false;
        DeactivatePlant();
>>>>>>> Stashed changes
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
                canReborn = true;
            }
        }
    }
    private void SpendingTime()
    {
        playerController.PlayerState = PlayerState.OnSpend;
        playerController.DrawLine(true);
    }
    private void ReturningTime()
    {
        canDeactivate = true;
        playerController.PlayerState = PlayerState.OnReturn;
        playerController.DrawLine(true);
    }

    #region Functions public


    public void DeactivatePlant()
    {
        isAlive = false;
        material.color = Color.gray;
<<<<<<< Updated upstream
        GameManager.Instance.CountPlantActivate(-1);

        mesh.mesh = baseState;

        canvas.gameObject.SetActive(false); //disable life bar
=======
        lifeTime = 0;
        isLifeMax = false;
        GameManager.Instance.CountPlantActivate(-1);
>>>>>>> Stashed changes
    }
    #endregion



}
