using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Bamboo : ObstacleBehaviour
{
    public bool CanChangeState;

    private float shapeWeight;

    private SkinnedMeshRenderer skinnedMesh;
    private void Start()
    {
        skinnedMesh = transform.GetComponent<SkinnedMeshRenderer>();

        if (IsActive) skinnedMesh.SetBlendShapeWeight(0, 100);
        else skinnedMesh.SetBlendShapeWeight(0, 0);

    }
    public override void ActivateObstacle()
    {
        
        IsActive = true;
        shapeWeight = 0;
        CanChangeState = true;
        print("Active");
        
    }
    public override void DeactivateObstacle()
    {
       
        IsActive = false;
        shapeWeight = 1;
        CanChangeState = true;
        print("Inactive");
    }

    private void Update()
    {
        if (CanChangeState)
        {
            if (IsActive)
            {
                skinnedMesh.SetBlendShapeWeight(0, Mathf.Lerp(100, 0, 2f));
                if (skinnedMesh.GetBlendShapeWeight(0)>=100)  CanChangeState = false;
            }
            else
            {
                skinnedMesh.SetBlendShapeWeight(0, Mathf.Lerp(0, 100, 2f));
                if (skinnedMesh.GetBlendShapeWeight(0) <= 0) CanChangeState = false;
            }
            
        }
    }
}
