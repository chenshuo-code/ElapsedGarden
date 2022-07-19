using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeStartPoint : MonoBehaviour
{
    public SkinnedMeshRenderer FirstLotus;

    bool triggerOnce = false;
    bool canActiveFirstLotus=false;

    private float shapeWeight;
    private float lerpSpeed = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerOnce)
        {
            if (other.CompareTag("Player"))
            {
                triggerOnce = true;
                canActiveFirstLotus = true;
            }
        }
    }
    private void Update()
    {
        if (canActiveFirstLotus)
        {
            lerpSpeed += Time.deltaTime;
            shapeWeight = Mathf.Lerp(0, 100, lerpSpeed);
            FirstLotus.SetBlendShapeWeight(0, shapeWeight);
            if (shapeWeight >= 100)
            {
                canActiveFirstLotus = false;
                lerpSpeed = 0;
            }
        }
    }
}
