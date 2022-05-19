using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehaviour : MonoBehaviour
{
    public float TimeCost; // time cost of this plant
    public Color AliveColor;

    private bool isAlive; //If this plant is activate in alive
    private Material material;
    private PlayerController playerController; //Get player controller
    private FirstTreeBehaviour firstTree; //Get first tree
    private void Start()
    {
        isAlive = false;

        playerController = GameManager.Instance.PlayerController;
        firstTree = GameManager.Instance.FirstTreeBehaviour;
        material = transform.GetComponent<Renderer>().material;
        material.color = Color.grey;


    }
    private void OnMouseEnter()
    {
        if (!isAlive && playerController.IsSpend)
        {
            ActivatePlant();
        }
    }

    private void ActivatePlant()
    {
        playerController.DrawLine(false); // Cancel draw line
        firstTree.GivingTime(TimeCost); // Minus time in FirstTree
        isAlive = true;
        material.color = AliveColor;
        GameManager.Instance.CountPlantActivate(1);
    }
    private void OnMouseDown()
    {
        ReturnTime();
    }

    private void ReturnTime()
    {
        playerController.IsSpend = false;
        playerController.DrawLine(true);
        playerController.TimeShipping += TimeCost;
        playerController.Plant = this;
    }

    public void DeactivatePlant()
    {
        isAlive = false;
        material.color = Color.gray;
        GameManager.Instance.CountPlantActivate(-1);
    }

}
