using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehaviour : MonoBehaviour
{
    public float timeCost; // time cost of this plant

    private bool isAlive; //If this plant is activate in alive
    private void Start()
    {
        isAlive = false;
    }
    private void OnMouseEnter()
    {
        if (!isAlive)
        {
            ActivatePlant();
        }

    }

    private void ActivatePlant()
    {
        GameManager.Instance.PlayerController.DrawLine(false); // Cancel draw line
        GameManager.Instance.FirstTreeBehaviour.GivingTime(timeCost); // Minus time in FirstTree
        isAlive = true;
    }


    //private void OnMouseDown()
    //{
    //    GameManager.Instance.PlayerController.DrawLine(true);
    //}
}
