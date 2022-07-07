using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Activate/Deactivate other plant in list the same time it has been activated
/// </summary>
public class Plant_AffectOthers : PlantBehaviour
{
    /// <summary>
    /// Plants to be activated when this plant active
    /// </summary>
    public List<PlantBehaviour> PlantsActivateList;
    /// <summary>
    /// Plants to be deactivated when this plant active
    /// </summary>
    public List<PlantBehaviour> PlantsDeactivateList;

    /// <summary>
    /// Obstacles to be deactivated after this plant active
    /// </summary>
    public List<ObstacleBehaviour> ObstaclesDeactivateList;

    /// <summary>
    /// Obstacles to be activated after this plant active
    /// </summary>
    public List<ObstacleBehaviour> ObstaclesActivateList;
    protected override void Init()
    {
        base.Init();
    }

    public override void ActivatePlant(bool needGrow)
    {
        base.ActivatePlant(false);
        foreach (PlantBehaviour plant in PlantsActivateList)
        {
            if (!plant.IsAlive)
            {
                plant.ActivatePlant(true);
            }
        }
        foreach (PlantBehaviour plant in PlantsDeactivateList)
        {
            if (plant.IsAlive)
            {
                plant.DeactivatePlant();
            }
        }
        foreach (ObstacleBehaviour Obstacle in ObstaclesActivateList)
        {
            if (!Obstacle.IsActive)
            {
                Obstacle.ActivateObstacle();
            }
        }     
        foreach (ObstacleBehaviour Obstacle in ObstaclesDeactivateList)
        {
            if (Obstacle.IsActive)
            {
                Obstacle.DeactivateObstacle();
            }
        }

    }
    public override void DeactivatePlant()
    {
        base.DeactivatePlant();
        foreach (PlantBehaviour plant in PlantsActivateList)
        {
            if (plant.IsAlive)
            {
                plant.DeactivatePlant();
            }
        }
        foreach (PlantBehaviour plant in PlantsDeactivateList)
        {
            if (!plant.IsAlive)
            {
                plant.ActivatePlant(true);
            }
        }
        foreach (ObstacleBehaviour Obstacle in ObstaclesActivateList)
        {
            if (Obstacle.IsActive)
            {
                Obstacle.DeactivateObstacle();
            }
        }
        foreach (ObstacleBehaviour Obstacle in ObstaclesDeactivateList)
        {
            if (!Obstacle.IsActive)
            {
                Obstacle.ActivateObstacle();
            }
        }
    }
}
