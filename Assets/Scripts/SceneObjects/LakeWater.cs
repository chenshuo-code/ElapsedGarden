using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeWater : MonoBehaviour
{
    public Transform StartPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController _player = other.GetComponent<PlayerController>();
            SoundManager.Instance.PlayOneshotTrack(SoundManager.Instance.FallInWaterSoundPath, _player.transform.position);
            _player.TeleportToPosition(StartPoint.position);
        }
    }
}
