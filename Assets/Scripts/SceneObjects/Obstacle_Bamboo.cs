using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Bamboo : ObstacleBehaviour
{
    [HideInInspector]public bool CanChangeState;

    private float shapeWeight;
    private float lerpSpeed=0;

    private SkinnedMeshRenderer skinnedMesh;
    private BoxCollider boxCollider;
    private void Start()
    {
        skinnedMesh = transform.GetComponent<SkinnedMeshRenderer>();
        boxCollider = transform.GetComponent<BoxCollider>();

        if (IsActive) skinnedMesh.SetBlendShapeWeight(0, 100);
        else skinnedMesh.SetBlendShapeWeight(0, 0);

    }
    public override void ActivateObstacle()
    {
        
        IsActive = true;
        boxCollider.enabled = true;
        CanChangeState = true;
        
    }
    public override void DeactivateObstacle()
    {
       
        IsActive = false;
        boxCollider.enabled = false;
        CanChangeState = true;
    }

    private void Update()
    {
        if (CanChangeState)
        {
            lerpSpeed += Time.deltaTime;
            if (IsActive)
            {
                shapeWeight = Mathf.Lerp(0, 100, lerpSpeed);
                skinnedMesh.SetBlendShapeWeight(0, shapeWeight);
                if (shapeWeight >= 100)
                {
                    CanChangeState = false;
                    lerpSpeed = 0;
                }
            }
            else
            {
                shapeWeight = Mathf.Lerp(100, 0, lerpSpeed);
                skinnedMesh.SetBlendShapeWeight(0,shapeWeight);
                if (shapeWeight <= 0)
                {
                    CanChangeState = false;
                    lerpSpeed = 0;
                }
            }
            
        }
    }
}
