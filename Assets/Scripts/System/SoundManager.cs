using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    //Event path

    //Player interaction
    [SerializeField] public EventReference MovingSoundPath;
    [SerializeField] public EventReference AliveMovingSoundPath; 
    [SerializeField] public EventReference TransmissionFluxSoundPath; 
    
    //Important point indication
    [SerializeField] public EventReference PiExitSoundPath; 

    //Plants feedbacks
    [SerializeField] public EventReference PlantActiveSoundPath;
    [SerializeField] public EventReference PlantPassSoundPath; 

    //Flux sounds
    [SerializeField] public EventReference FluxStateChangeSoundPath; 

    //Check Point sounds
    [SerializeField] public EventReference CheckPointInactiveStateSoundPath; 
    [SerializeField] public EventReference CheckPointActiveStateSoundPath; 
    [SerializeField] public EventReference CheckPointActivateSoundPath; 
    [SerializeField] public EventReference RechargingFluxSoundPath; 
    //[SerializeField] public EventReference RechargeFluxFinishSoundPath; 
    [SerializeField] public EventReference cancelRechargeFluxSoundPath; 


    //Sound event

    /// <summary>
    /// Basic moving sound
    /// </summary>
    public EventInstance MovingSound;
    /// <summary>
    /// Sound of moving when player alive
    /// </summary>
    public EventInstance AliveMovingSound;
    /// <summary>
    /// Sound of transmission flux to plant
    /// </summary>
    public EventInstance TransmissionFluxSound;
    /// <summary>
    /// Sound of indication exit
    /// </summary>
    public EventInstance PiExitSound;
    /// <summary>
    /// Sound feed back when plant active
    /// </summary>
    public EventInstance PlantActiveSound;
    /// <summary>
    /// Sound when we pass a plant
    /// </summary>
    public EventInstance PlantPassSound;
    /// <summary>
    /// Sound feedback when flux state change
    /// </summary>
    public EventInstance FluxStateChangeSound;
    /// <summary>
    /// Sound when check point is inactived
    /// </summary>
    public EventInstance CheckPointInactiveStateSound;
    /// <summary>
    /// Sound when check point is actived
    /// </summary>
    public EventInstance CheckPointActiveStateSound;
    /// <summary>
    /// Onshot feedback sound on Check point activate
    /// </summary>
    public EventInstance CheckPointActivateSound;
    /// <summary>
    /// Sound when player recharging flux at check point
    /// </summary>
    public EventInstance RechargingFluxSound;
    ///// <summary>
    ///// Sound when recharging flux finish
    ///// </summary>
    //public EventInstance RechargeFluxFinishSound;
    /// <summary>
    /// Feedback sound when player cancel flux recharge
    /// </summary>
    public EventInstance cancelRechargeFluxSound;

    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        MovingSound = RuntimeManager.CreateInstance(MovingSoundPath);
        AliveMovingSound = RuntimeManager.CreateInstance(AliveMovingSoundPath);
        TransmissionFluxSound = RuntimeManager.CreateInstance(TransmissionFluxSoundPath);
        PiExitSound = RuntimeManager.CreateInstance(PiExitSoundPath);
        PlantActiveSound = RuntimeManager.CreateInstance(PlantActiveSoundPath);
        PlantPassSound = RuntimeManager.CreateInstance(PlantPassSoundPath);
        FluxStateChangeSound = RuntimeManager.CreateInstance(FluxStateChangeSoundPath);
        CheckPointInactiveStateSound = RuntimeManager.CreateInstance(CheckPointInactiveStateSoundPath);
        CheckPointActiveStateSound = RuntimeManager.CreateInstance(CheckPointActiveStateSoundPath);
        CheckPointActivateSound = RuntimeManager.CreateInstance(CheckPointActivateSoundPath);
        RechargingFluxSound = RuntimeManager.CreateInstance(RechargingFluxSoundPath);
        //RechargeFluxFinishSound = RuntimeManager.CreateInstance(RechargeFluxFinishSoundPath);
        cancelRechargeFluxSound = RuntimeManager.CreateInstance(cancelRechargeFluxSoundPath);
    }

    /// <summary>
    /// Create fmod event instance
    /// </summary>
    /// <param name="eventPath">Event path</param>
    /// <returns>Fmod event instance</returns>
    public EventInstance CreateInstance(string eventPath)
    {
        var instance = RuntimeManager.CreateInstance(eventPath);
        return instance;
    }
        
    /// <summary>
    /// Play one shot track at given place
    /// </summary>
    /// <param name="eventPath">Fmod event path</param>
    /// <param name="position">Position to play track</param>
    public void PlayOneshotTrack(EventReference eventPath, Vector3 position)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventPath, position);
        
    }
}
