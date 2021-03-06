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
    [SerializeField] public EventReference FallInWaterSoundPath; 
    
    //Important point indication
    [SerializeField] public EventReference PiExitSoundPath; 
    [SerializeField] public EventReference PiDoorOpenSoundPath; 

    //Plants feedbacks
    [SerializeField] public EventReference PlantActiveSoundPath;
    [SerializeField] public EventReference PlantPassSoundPath; 
    [SerializeField] public EventReference PlantChaineActiveSoundPath; 
    [SerializeField] public EventReference BambooActiveSoundPath; 
    [SerializeField] public EventReference LotusActiveSoundPath;

    //Obstacle feedbacks
    [SerializeField] public EventReference TorchActiveSoundPath;
    [SerializeField] public EventReference SandSoundPath;

    //Flux sounds
    [SerializeField] public EventReference FluxStateChangeSoundPath; 

    //Check Point sounds
    [SerializeField] public EventReference CheckPointInactiveStateSoundPath; 
    [SerializeField] public EventReference CheckPointActiveStateSoundPath; 
    [SerializeField] public EventReference CheckPointActivateSoundPath; 
    [SerializeField] public EventReference RechargingFluxSoundPath; 
    [SerializeField] public EventReference cancelRechargeFluxSoundPath; 

    //BGM
    [SerializeField] public EventReference BackGroundMusicSoundPath; 

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
    public EventInstance FallInWaterSound;


    /// <summary>
    /// Sound of indication exit
    /// </summary>
    public EventInstance PiExitSound;
    public EventInstance PiDoorOpenSound;


    /// <summary>
    /// Sound feed back when plant active
    /// </summary>
    public EventInstance PlantActiveSound;
    /// <summary>
    /// Sound when we pass a plant
    /// </summary>
    public EventInstance PlantPassSound;
    public EventInstance PlantChaineActiveSound;


    /// <summary>
    /// Sound feedback when flux state change
    /// </summary>
    public EventInstance FluxStateChangeSound;

    /// <summary>
    /// Sound when check point is inactived
    /// </summary>
    /// 
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
    /// <summary>
    /// Feedback sound when player cancel flux recharge
    /// </summary>
    public EventInstance cancelRechargeFluxSound;

    public EventInstance BackGroundMusicSound;

    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        MovingSound = RuntimeManager.CreateInstance(MovingSoundPath);
        AliveMovingSound = RuntimeManager.CreateInstance(AliveMovingSoundPath);
        TransmissionFluxSound = RuntimeManager.CreateInstance(TransmissionFluxSoundPath);
        FallInWaterSound = RuntimeManager.CreateInstance(FallInWaterSoundPath);
 
        PiExitSound = RuntimeManager.CreateInstance(PiExitSoundPath);
        PiDoorOpenSound = RuntimeManager.CreateInstance(PiDoorOpenSoundPath);
        
        PlantActiveSound = RuntimeManager.CreateInstance(PlantActiveSoundPath);
        PlantPassSound = RuntimeManager.CreateInstance(PlantPassSoundPath);
        PlantChaineActiveSound = RuntimeManager.CreateInstance(PlantChaineActiveSoundPath);

        FluxStateChangeSound = RuntimeManager.CreateInstance(FluxStateChangeSoundPath);

        CheckPointInactiveStateSound = RuntimeManager.CreateInstance(CheckPointInactiveStateSoundPath);
        CheckPointActiveStateSound = RuntimeManager.CreateInstance(CheckPointActiveStateSoundPath);
        CheckPointActivateSound = RuntimeManager.CreateInstance(CheckPointActivateSoundPath);
        RechargingFluxSound = RuntimeManager.CreateInstance(RechargingFluxSoundPath);
        cancelRechargeFluxSound = RuntimeManager.CreateInstance(cancelRechargeFluxSoundPath);

        BackGroundMusicSound = RuntimeManager.CreateInstance(BackGroundMusicSoundPath);

    }

    /// <summary>
    /// Create fmod event instance
    /// </summary>
    /// <param name="eventPath">Event path</param>
    /// <returns>Fmod event instance</returns>
    public EventInstance CreateInstance(EventReference _ref)
    {
        var instance = RuntimeManager.CreateInstance(_ref);
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
        print("One Shot Play");
    }
}
