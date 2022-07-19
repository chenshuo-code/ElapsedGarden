using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check point, for functions launche a round
/// </summary>
public class CheckPoint : MonoBehaviour
{
    /// <summary>
    /// Max flux to be Increased on check point activate 
    /// </summary>
    public float RewardFlux;
    /// <summary>
    /// default active
    /// </summary>
    public bool DefaultActive;

    public float TreeFlux = 600;

    private bool isActive;

    private GuideFluxBehaviour guideFlux;
    private PlayerController playerController;

    private void Start()
    {
        isActive = false;

        guideFlux = GameManager.Instance.GuideFlux;
        playerController = GameManager.Instance.PlayerController;


        if (DefaultActive) ActiveCheckPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isActive)
            {
                ActiveCheckPoint();
            }
            else
            {
                ReloadCheckPoint();
            }
        }
    }

    /// <summary>
    /// Reload from a Active CheckPoint
    /// </summary>
    private void ReloadCheckPoint()
    {
        if (TreeFlux>= guideFlux.GetFluxToRecharge())
        {
            TreeFlux = TreeFlux - (guideFlux.GetFluxToRecharge());
            GameManager.Instance.CheckGame();
        }
        else
        {
            print("this tree is deaded");
        }
    }
    public void ActiveCheckPoint()
    {
        isActive = true;

        guideFlux.IncreaseMaxFlux(RewardFlux); //Add max flux
        GameManager.Instance.ActivateCheckPoint(this);

        GameManager.Instance.ObstacleDoor.CheckPointResolve(); //Record To Open Door
        print("CheckPointActive");
        gameObject.layer = LayerMask.NameToLayer("Color");
    }
    
}
