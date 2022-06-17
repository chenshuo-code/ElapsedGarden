using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_EatFlux : MonoBehaviour
{
    public float EatSpeed;

    private bool canActivate;

    private GuideFluxBehaviour guideFlux; //Get GuideFlux
    private void Start()
    {
        guideFlux = GameManager.Instance.GuideFlux;
    }

    private void Update()
    {
        if (canActivate)
        {
            guideFlux.ReduceFlux(EatSpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canActivate = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}
