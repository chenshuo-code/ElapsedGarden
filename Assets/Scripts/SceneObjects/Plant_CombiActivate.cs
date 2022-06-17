using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This plant will be activated when the other plants active
/// </summary>
public class Plant_CombiActivate : PlantBehaviour
{
    private int suscombiPlantsCount = 0;

    /// <summary>
    /// Register suscombiplants count at game start
    /// </summary>
    public void RegisterSuscombiPlants()
    {
        suscombiPlantsCount++;
    }

    /// <summary>
    /// Reduce Plants Count when a suscombiplant activate
    /// </summary>
    public void ReduceSuscombiPlantsCount()
    {
        suscombiPlantsCount--;
        if (suscombiPlantsCount <=0)
        {
            this.ActivatePlant();
        }
    }
}
