using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class SoundManager : MonoBehaviour
{
    FMOD.Studio.EventInstance eventInstance;
    public static SoundManager Instance;

    //Event path

    [SerializeField] public EventReference MovingSoundPath;
    [SerializeField] public EventReference AliveMovingSoundPath; 

    //Sound event

    /// <summary>
    /// Basic moving sound
    /// </summary>
    public EventInstance MovingSound;
    /// <summary>
    /// Sound of moving when player alive
    /// </summary>
    public EventInstance AliveMovingSound;
    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        MovingSound = RuntimeManager.CreateInstance(MovingSoundPath);
        AliveMovingSound = RuntimeManager.CreateInstance(AliveMovingSoundPath);
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
    public void PlayOneshotTrack(string eventPath, Vector3 position)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventPath, position);
        
    }
}
