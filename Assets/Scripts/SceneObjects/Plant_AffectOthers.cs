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

    protected override void Init()
    {
        base.Init();
    }

    public override void ActivatePlant()
    {
        base.ActivatePlant();
        foreach (PlantBehaviour plant in PlantsActivateList)
        {
            if (!plant.IsAlive)
            {
                plant.ActivatePlant();
            }
        }
        foreach (PlantBehaviour plant in PlantsDeactivateList)
        {
            if (plant.IsAlive)
            {
                plant.DeactivatePlant();
            }
        }
    }
}
