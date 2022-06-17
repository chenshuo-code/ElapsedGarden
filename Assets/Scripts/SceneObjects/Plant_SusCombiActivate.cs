using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_SusCombiActivate : PlantBehaviour
{
    public Plant_CombiActivate PlantToActive;
    protected override void Init()
    {
        base.Init();
        PlantToActive.RegisterSuscombiPlants();
    }
    public override void ActivatePlant()
    {
        base.ActivatePlant();
        PlantToActive.ReduceSuscombiPlantsCount();
    }
    public override void DeactivatePlant()
    {
        base.DeactivatePlant();
        PlantToActive.RegisterSuscombiPlants();

    }
}
