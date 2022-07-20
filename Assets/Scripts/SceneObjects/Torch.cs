using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public Obstacle_Door Door;

    private Transform fire;

    private bool isActive=false;
    private void Start()
    {
        fire = transform.GetChild(0);
        fire.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!isActive)
        {
            isActive = true;
            fire.gameObject.SetActive(true);
            if (Door!=null) Door.TorchResolve();
            SoundManager.Instance.PlayOneshotTrack(SoundManager.Instance.TorchActiveSoundPath,transform.position);
        }
    }
}
