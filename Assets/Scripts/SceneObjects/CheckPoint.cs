using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
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

    public Obstacle_Door Door;

    private bool isActive;

    private GuideFluxBehaviour guideFlux;
    private PlayerController playerController;

    private EventInstance inactiveSound;

    private void Start()
    {
        isActive = false;

        guideFlux = GameManager.Instance.GuideFlux;
        playerController = GameManager.Instance.PlayerController;
        

        inactiveSound = RuntimeManager.CreateInstance(SoundManager.Instance.CheckPointInactiveStateSoundPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(inactiveSound, this.transform);
        inactiveSound.start();

        if (DefaultActive) ActiveCheckPoint();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isActive)
            {
                ActiveCheckPoint();
                if(Door!=null) Door.CheckPointResolve(); //Record To Open Door
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

        gameObject.layer = LayerMask.NameToLayer("Color");
        inactiveSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        FMODUnity.RuntimeManager.PlayOneShot(SoundManager.Instance.CheckPointActiveStateSoundPath, this.transform.position);

       
    }
    
}
