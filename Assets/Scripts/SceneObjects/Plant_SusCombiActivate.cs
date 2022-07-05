using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_SusCombiActivate : PlantBehaviour
{
    public Plant_CombiActivate PlantToActive;
    public Obstacle_CombiActive obstacleToDeactive;
    
    protected override void Init()
    {
        base.Init();
        PlantToActive.RegisterSuscombiPlants();
    }
    public override void ActivatePlant()
    {
        base.ActivatePlant();
        if (PlantToActive!=null) PlantToActive.ReduceSuscombiPlantsCount();

        if(obstacleToDeactive!=null) obstacleToDeactive.ReduceSuscombiPlantsCount();
    }
    public override void DeactivatePlant()
    {
        base.DeactivatePlant();
        PlantToActive.RegisterSuscombiPlants();
        PlantToActive.DeactivatePlant();

        if (PlantToActive != null) obstacleToDeactive.RegisterSuscombiPlants();
        if (obstacleToDeactive != null) obstacleToDeactive.ActivateObstacle();
    }
}
