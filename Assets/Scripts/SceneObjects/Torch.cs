using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
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
            GameManager.Instance.ObstacleDoor.TorchResolve();
            SoundManager.Instance.PlayOneshotTrack(SoundManager.Instance.TorchActiveSoundPath,transform.position);
        }
    }
}
